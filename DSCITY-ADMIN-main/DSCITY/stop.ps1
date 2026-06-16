Get-Process | Where-Object {
    $_.ProcessName -eq 'DSCITY' -or
    ($_.ProcessName -eq 'dotnet' -and $_.Path -eq 'C:\Program Files\dotnet\dotnet.exe')
} | ForEach-Object {
    try {
        $cmd = (Get-CimInstance Win32_Process -Filter "ProcessId = $($_.Id)").CommandLine
        if ($cmd -and $cmd -like '*DSCITY*') {
            Stop-Process -Id $_.Id -Force
            Write-Host "Stopped process $($_.Id)"
        }
    } catch {
    }
}
