# 批量添加 table-footer 到所有包含 data-table-container 的 .razor 文件

$pagesPath = "e:\AICode\WESSystemQD\WmsPlus\Pages"
$files = Get-ChildItem -Path $pagesPath -Filter "*.razor" -Recurse

$tableFooter = @'
            <!-- 表格底部栏 -->
            <div class="table-footer">
                <div class="footer-left">
                    <span class="footer-label">分页栏位字段：</span>
                    <div class="footer-checkboxes">
                        @foreach (var col in columnConfigs.Where(c => c.Visible))
                        {
                            <label class="footer-checkbox-item">
                                <input type="checkbox" checked />
                                <span>@col.DisplayName</span>
                            </label>
                        }
                    </div>
                </div>
                <div class="footer-right">
                    <span class="record-count">共 @dataSource.Count 条记录</span>
                    <div class="pagination-info"></div>
                </div>
            </div>
'@

$simpleFooter = @'
            <!-- 表格底部栏 -->
            <div class="table-footer">
                <div class="footer-left">
                    <span class="footer-label">分页栏位字段：</span>
                    <div class="footer-checkboxes">
                    </div>
                </div>
                <div class="footer-right">
                    <span class="record-count">共 @dataSource.Count 条记录</span>
                    <div class="pagination-info"></div>
                </div>
            </div>
'@

$oldPattern = @'
                </table>
            </div>
        </div>
    </div>
'@

$modifiedCount = 0
$errorCount = 0
$skippedCount = 0

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw -Encoding UTF8
    
    # 检查是否包含 data-table-container
    if ($content -match 'class="data-table-container"') {
        # 检查是否已经包含 table-footer（避免重复添加）
        if ($content -match 'class="table-footer"') {
            Write-Host "跳过 (已包含 table-footer): $($file.Name)" -ForegroundColor Yellow
            $skippedCount++
            continue
        }
        
        # 检查是否包含目标模式
        if ($content.Contains($oldPattern)) {
            # 检查文件是否有 columnConfigs 变量
            if ($content -match 'columnConfigs') {
                # 使用标准 footer（带 columnConfigs）
                $newPattern = $oldPattern + $tableFooter + @'
        </div>
    </div>
'@
                
                $newContent = $content.Replace($oldPattern, $newPattern)
                
                # 验证替换后的 div 平衡性
                $openDivs = ([regex]::Matches($newContent, '<div')).Count
                $closeDivs = ([regex]::Matches($newContent, '</div>')).Count
                
                if ($openDivs -eq $closeDivs) {
                    [System.IO.File]::WriteAllText($file.FullName, $newContent, [System.Text.Encoding]::UTF8)
                    Write-Host "成功修改: $($file.Name)" -ForegroundColor Green
                    $modifiedCount++
                }
                else {
                    Write-Host "错误: $($file.Name) - div 标签不匹配 (open=$openDivs, close=$closeDivs)" -ForegroundColor Red
                    $errorCount++
                }
            }
            else {
                # 使用简化 footer（不带 columnConfigs）
                $simpleNewPattern = $oldPattern + $simpleFooter + @'
        </div>
    </div>
'@
                
                $newContent = $content.Replace($oldPattern, $simpleNewPattern)
                
                $openDivs = ([regex]::Matches($newContent, '<div')).Count
                $closeDivs = ([regex]::Matches($newContent, '</div>')).Count
                
                if ($openDivs -eq $closeDivs) {
                    [System.IO.File]::WriteAllText($file.FullName, $newContent, [System.Text.Encoding]::UTF8)
                    Write-Host "成功修改 (简化版): $($file.Name)" -ForegroundColor Cyan
                    $modifiedCount++
                }
                else {
                    Write-Host "错误: $($file.Name) - div 标签不匹配 (open=$openDivs, close=$closeDivs)" -ForegroundColor Red
                    $errorCount++
                }
            }
        }
        else {
            Write-Host "跳过 (未找到目标模式): $($file.Name)" -ForegroundColor DarkGray
            $skippedCount++
        }
    }
}

Write-Host "`n========== 处理完成 ==========" -ForegroundColor White
Write-Host "修改文件数: $modifiedCount" -ForegroundColor Green
Write-Host "跳过文件数: $skippedCount" -ForegroundColor Yellow
Write-Host "错误文件数: $errorCount" -ForegroundColor Red
