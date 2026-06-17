# 检查 IIS URL Rewrite Module 是否安装

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "检查 IIS URL Rewrite Module" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 方法1：检查注册表
$rewriteRegPath = "HKLM:\SOFTWARE\Microsoft\IIS Extensions\Rewrite"
if (Test-Path $rewriteRegPath) {
    Write-Host "✓ URL Rewrite Module 已安装" -ForegroundColor Green
    Get-ItemProperty $rewriteRegPath | Select-Object MajorVersion, MinorVersion
} else {
    Write-Host "✗ URL Rewrite Module 未安装！" -ForegroundColor Red
    Write-Host ""
    Write-Host "请下载并安装：" -ForegroundColor Yellow
    Write-Host "https://www.iis.net/downloads/microsoft/url-rewrite" -ForegroundColor Cyan
    exit 1
}

# 方法2：检查模块文件
$modulePath = "$env:windir\System32\inetsrv\rewrite.dll"
if (Test-Path $modulePath) {
    Write-Host "`n✓ rewrite.dll 存在: $modulePath" -ForegroundColor Green
} else {
    Write-Host "`n✗ rewrite.dll 不存在" -ForegroundColor Red
}

Write-Host ""
Write-Host "按任意键退出..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
