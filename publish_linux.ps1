Get-ChildItem -Path "dist\linux" | Remove-Item -Force -Recurse -ErrorAction Ignore

dotnet publish -c Release -r linux-x64 -p:CopyOutputSymbolsToPublishDirectory=false --self-contained true -o dist\linux src\OikClientDraft.csproj

Get-ChildItem -Path "dist\linux\*.pdb" | Remove-Item -Force -ErrorAction Ignore
Get-ChildItem -Path "dist\linux\*" -Directory | Remove-Item -Force -Recurse -ErrorAction Ignore