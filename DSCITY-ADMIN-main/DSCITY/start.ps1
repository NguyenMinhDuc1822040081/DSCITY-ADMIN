$projectDir = Split-Path -Parent $MyInvocation.MyCommand.Path

Get-Process | Where-Object {
    $_.ProcessName -eq 'DSCITY' -or
    ($_.ProcessName -eq 'dotnet' -and $_.Path -eq 'C:\Program Files\dotnet\dotnet.exe')
} | ForEach-Object {
    try {
        $cmd = (Get-CimInstance Win32_Process -Filter "ProcessId = $($_.Id)").CommandLine
        if ($cmd -and $cmd -like '*DSCITY*') {
            Stop-Process -Id $_.Id -Force
        }
    } catch {
    }
}

Start-Sleep -Seconds 1
Start-Process -FilePath 'dotnet' -ArgumentList 'run' -WorkingDirectory $projectDir
Write-Host 'DSCITY is starting at http://localhost:5285'
