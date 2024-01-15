Get-ChildItem -Path .\k8s\*.yaml | ForEach-Object { kubectl apply -f $_.FullName }
