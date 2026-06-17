# IIS ASP.NET Core 部署诊断和修复脚本
# 用于检查和修复 WMS API 站点的 aspNetCore 处理程序问题

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "IIS ASP.NET Core 部署诊断工具" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 1. 检查 AspNetCoreModuleV2 是否安装
Write-Host "[1] 检查 AspNetCoreModuleV2 模块..." -ForegroundColor Yellow
$modulePath = "$env:windir\System32\inetsrv\aspnetcorev2.dll"
if (Test-Path $modulePath) {
    Write-Host "✓ AspNetCoreModuleV2 已安装" -ForegroundColor Green
} else {
    Write-Host "✗ AspNetCoreModuleV2 未安装！" -ForegroundColor Red
    Write-Host "请下载安装 ASP.NET Core Hosting Bundle:" -ForegroundColor Yellow
    Write-Host "https://dotnet.microsoft.com/download/dotnet/8.0/runtime?osType=windows&arch=x64&type=hosting-bundle" -ForegroundColor Cyan
    exit 1
}

# 2. 检查 web.config 是否存在
Write-Host ""
Write-Host "[2] 检查 web.config 文件..." -ForegroundColor Yellow
$apiPath = "e:\AICode\WESSystemQD\publish\api"
$webConfigPath = Join-Path $apiPath "web.config"
if (Test-Path $webConfigPath) {
    Write-Host "✓ web.config 存在: $webConfigPath" -ForegroundColor Green
    
    # 检查内容是否包含 aspNetCore
    $content = Get-Content $webConfigPath -Raw
    if ($content -match 'aspNetCore') {
        Write-Host "✓ web.config 包含 aspNetCore 配置" -ForegroundColor Green
    } else {
        Write-Host "✗ web.config 缺少 aspNetCore 配置！" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "✗ web.config 不存在！" -ForegroundColor Red
    Write-Host "预期位置: $webConfigPath" -ForegroundColor Yellow
    exit 1
}

# 3. 检查 IIS 站点配置
Write-Host ""
Write-Host "[3] 检查 IIS 站点配置..." -ForegroundColor Yellow
try {
    Import-Module WebAdministration -ErrorAction Stop
    
    # 检查 api 子应用是否存在
    $apiApp = Get-WebApplication -Site "WMS" -Name "api" -ErrorAction SilentlyContinue
    if ($apiApp) {
        Write-Host "✓ WMS/api 子应用存在" -ForegroundColor Green
        Write-Host "  物理路径: $($apiApp.PhysicalPath)" -ForegroundColor Gray
        
        # 检查应用程序池
        $appPool = $apiApp.applicationPool
        Write-Host "  应用程序池: $appPool" -ForegroundColor Gray
        
        # 获取应用程序池状态
        $poolState = (Get-WebAppPoolState -Name $appPool).Value
        Write-Host "  应用程序池状态: $poolState" -ForegroundColor Gray
    } else {
        Write-Host "✗ WMS/api 子应用不存在！" -ForegroundColor Red
        Write-Host "请在 IIS 管理器中将 publish/api 目录转换为应用程序" -ForegroundColor Yellow
        exit 1
    }
} catch {
    Write-Host "✗ 无法访问 IIS 配置（需要管理员权限）" -ForegroundColor Red
    Write-Host "请以管理员身份运行此脚本" -ForegroundColor Yellow
    exit 1
}

# 4. 提供修复建议
Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "诊断完成 - 修复建议" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "如果 IIS 管理器中仍看不到 aspNetCore 处理程序，请执行以下步骤：" -ForegroundColor Yellow
Write-Host ""
Write-Host "步骤 1: 回收应用程序池" -ForegroundColor White
Write-Host "  在 IIS 管理器中：" -ForegroundColor Gray
Write-Host "  1. 展开 '应用程序池'" -ForegroundColor Gray
Write-Host "  2. 找到 '$appPool'" -ForegroundColor Gray
Write-Host "  3. 右键 → '回收'" -ForegroundColor Gray
Write-Host ""
Write-Host "  或在 PowerShell 中运行（管理员）：" -ForegroundColor Gray
Write-Host "  Restart-WebAppPool -Name '$appPool'" -ForegroundColor Cyan
Write-Host ""

Write-Host "步骤 2: 重启 IIS（如果步骤 1 无效）" -ForegroundColor White
Write-Host "  在 PowerShell 中运行（管理员）：" -ForegroundColor Gray
Write-Host "  iisreset" -ForegroundColor Cyan
Write-Host ""

Write-Host "步骤 3: 验证配置" -ForegroundColor White
Write-Host "  在 IIS 管理器中：" -ForegroundColor Gray
Write-Host "  1. 展开 WMS → api" -ForegroundColor Gray
Write-Host "  2. 双击 '处理程序映射'" -ForegroundColor Gray
Write-Host "  3. 确认列表中有 'aspNetCore' 条目" -ForegroundColor Gray
Write-Host ""

Write-Host "按任意键退出..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
