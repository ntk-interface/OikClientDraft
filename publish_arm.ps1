Get-ChildItem -Path "dist\arm" | Remove-Item -Force -Recurse -ErrorAction Ignore

dotnet publish -c Release -r linux-arm64 -p:CopyOutputSymbolsToPublishDirectory=false -p:PublishSingleFile=true --self-contained true -o dist\arm src\OikClientDraft.csproj

Get-ChildItem -Path "dist\arm\*.pdb" | Remove-Item -Force -ErrorAction Ignore
Get-ChildItem -Path "dist\arm\*" -Directory | Remove-Item -Force -Recurse -ErrorAction Ignore