# WMS 部署完整诊断脚本

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "WMS 部署诊断工具" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$hasError = $false

# 1. 检查 URL Rewrite Module
Write-Host "[1] 检查 URL Rewrite Module..." -ForegroundColor Yellow
$rewriteRegPath = "HKLM:\SOFTWARE\Microsoft\IIS Extensions\Rewrite"
if (Test-Path $rewriteRegPath) {
    Write-Host "   ✓ URL Rewrite Module 已安装" -ForegroundColor Green
} else {
    Write-Host "   ✗ URL Rewrite Module 未安装！" -ForegroundColor Red
    $hasError = $true
}

# 2. 检查 AspNetCoreModuleV2
Write-Host "`n[2] 检查 AspNetCoreModuleV2..." -ForegroundColor Yellow
$modulePath = "$env:windir\System32\inetsrv\aspnetcorev2.dll"
if (Test-Path $modulePath) {
    Write-Host "   ✓ AspNetCoreModuleV2 已安装" -ForegroundColor Green
} else {
    Write-Host "   ✗ AspNetCoreModuleV2 未安装！" -ForegroundColor Red
    $hasError = $true
}

# 3. 检查 IIS 站点和应用程序
Write-Host "`n[3] 检查 IIS 配置..." -ForegroundColor Yellow
try {
    Import-Module WebAdministration -ErrorAction Stop
    
    # 检查 WMS 主站点
    $wmsSite = Get-Website -Name "WMS" -ErrorAction SilentlyContinue
    if ($wmsSite) {
        Write-Host "   ✓ WMS 站点存在" -ForegroundColor Green
        Write-Host "     物理路径: $($wmsSite.PhysicalPath)" -ForegroundColor Gray
        Write-Host "     绑定: $($wmsSite.bindings.Collection | Select-Object -ExpandProperty bindingInformation)" -ForegroundColor Gray
    } else {
        Write-Host "   ✗ WMS 站点不存在！" -ForegroundColor Red
        $hasError = $true
    }
    
    # 检查 api 子应用
    $apiApp = Get-WebApplication -Site "WMS" -Name "api" -ErrorAction SilentlyContinue
    if ($apiApp) {
        Write-Host "   ✓ api 子应用存在" -ForegroundColor Green
        Write-Host "     物理路径: $($apiApp.PhysicalPath)" -ForegroundColor Gray
        Write-Host "     应用程序池: $($apiApp.applicationPool)" -ForegroundColor Gray
        
        # 检查 web.config
        $webConfigPath = Join-Path $apiApp.PhysicalPath "web.config"
        if (Test-Path $webConfigPath) {
            $content = Get-Content $webConfigPath -Raw
            if ($content -match "aspNetCore") {
                Write-Host "     ✓ web.config 包含 aspNetCore 配置" -ForegroundColor Green
            } else {
                Write-Host "     ✗ web.config 缺少 aspNetCore 配置" -ForegroundColor Red
                $hasError = $true
            }
        } else {
            Write-Host "     ✗ web.config 不存在" -ForegroundColor Red
            $hasError = $true
        }
    } else {
        Write-Host "   ✗ api 子应用不存在！" -ForegroundColor Red
        $hasError = $true
    }
    
    # 检查应用程序池状态
    $poolName = if ($apiApp) { $apiApp.applicationPool } else { "WMSAppPool" }
    $poolState = (Get-WebAppPoolState -Name $poolName -ErrorAction SilentlyContinue).Value
    if ($poolState -eq "Started") {
        Write-Host "   ✓ 应用程序池 '$poolName' 正在运行" -ForegroundColor Green
    } else {
        Write-Host "   ✗ 应用程序池 '$poolName' 状态: $poolState" -ForegroundColor Red
        $hasError = $true
    }
    
} catch {
    Write-Host "   ✗ 无法访问 IIS 配置（需要管理员权限）" -ForegroundColor Red
    $hasError = $true
}

# 4. 检查 wwwroot 的 web.config
Write-Host "`n[4] 检查 wwwroot web.config..." -ForegroundColor Yellow
$wwwrootWebConfig = "D:\WMSSystem\publish\wasm\web.config"
if (Test-Path $wwwrootWebConfig) {
    Write-Host "   ✓ wwwroot web.config 存在" -ForegroundColor Green
    $content = Get-Content $wwwrootWebConfig -Raw
    if ($content -match "Exclude API requests") {
        Write-Host "   ✓ 包含 API 排除规则" -ForegroundColor Green
    } else {
        Write-Host "   ✗ 缺少 API 排除规则" -ForegroundColor Red
        $hasError = $true
    }
} else {
    Write-Host "   ✗ wwwroot web.config 不存在" -ForegroundColor Red
    $hasError = $true
}

# 5. 检查 appsettings.json
Write-Host "`n[5] 检查前端配置..." -ForegroundColor Yellow
$appSettingsPath = "D:\WMSSystem\publish\wasm\wwwroot\appsettings.json"
if (Test-Path $appSettingsPath) {
    Write-Host "   ✓ appsettings.json 存在" -ForegroundColor Green
    $content = Get-Content $appSettingsPath -Raw
    Write-Host "   内容: $content" -ForegroundColor Gray
} else {
    Write-Host "   ✗ appsettings.json 不存在" -ForegroundColor Red
    $hasError = $true
}

# 6. 测试 API 端点
Write-Host "`n[6] 测试 API 端点..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "http://localhost:8190/api/auth/login" `
        -Method POST `
        -ContentType "application/json" `
        -Body '{"username":"test","password":"test"}' `
        -UseBasicParsing `
        -TimeoutSec 5
    Write-Host "   ✓ API 响应状态: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "   响应: $($response.Content.Substring(0, [Math]::Min(100, $response.Content.Length)))..." -ForegroundColor Gray
} catch {
    Write-Host "   ✗ API 调用失败: $($_.Exception.Message)" -ForegroundColor Red
    $hasError = $true
}

# 7. 检查 API 日志
Write-Host "`n[7] 检查 API 日志..." -ForegroundColor Yellow
$logDir = "D:\WMSSystem\publish\api\logs"
if (Test-Path $logDir) {
    $logFiles = Get-ChildItem "$logDir\*.log" -ErrorAction SilentlyContinue
    if ($logFiles) {
        $latestLog = $logFiles | Sort-Object LastWriteTime -Descending | Select-Object -First 1
        Write-Host "   最新日志: $($latestLog.Name)" -ForegroundColor Gray
        if ($latestLog.Length -gt 0) {
            Write-Host "   最后 10 行:" -ForegroundColor Gray
            Get-Content $latestLog.FullName -Tail 10 | ForEach-Object { Write-Host "   $_" -ForegroundColor DarkGray }
        } else {
            Write-Host "   ⚠ 日志文件为空（API 可能未启动）" -ForegroundColor Yellow
        }
    } else {
        Write-Host "   ⚠ 无日志文件" -ForegroundColor Yellow
    }
} else {
    Write-Host "    日志目录不存在" -ForegroundColor Yellow
}

# 总结
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "诊断完成" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if (-not $hasError) {
    Write-Host "`n✓ 所有检查通过！" -ForegroundColor Green
} else {
    Write-Host "`n✗ 发现错误，请查看上述详细信息" -ForegroundColor Red
}

Write-Host ""
Write-Host "按任意键退出..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
