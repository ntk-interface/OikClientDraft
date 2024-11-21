Get-ChildItem -Path "dist\windows" | Remove-Item -Force -Recurse -ErrorAction Ignore

dotnet publish -c Release -r win-x64 -p:CopyOutputSymbolsToPublishDirectory=false -p:PublishSingleFile=true --self-contained true -o dist\windows src\OikClientDraft.csproj

Get-ChildItem -Path "dist\windows\*.pdb" | Remove-Item -Force -ErrorAction Ignore
Get-ChildItem -Path "dist\windows\*" -Directory | Remove-Item -Force -Recurse -ErrorAction Ignore