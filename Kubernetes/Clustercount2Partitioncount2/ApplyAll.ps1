
Get-ChildItem -Filter *.yaml | ForEach-Object {
    kubectl apply -f $_.FullName
}
