
Get-ChildItem -Filter *.yaml | ForEach-Object {
    kubectl apply -f $_.FullName --validate=false
}
kubectl set env deployment/expose-api AllowedHosts='*' ConnectionStrings='Data Source=expose-api-db,1433;Initial Catalog=Userdata;User ID=sa;Password=RootRoot.1; Encrypt=False;' HowManyPartition='1' HowManyCluster='1' Jwt='c5eKUcbKfkzGq6HfpnFhP7/G2pgS3S++YH33ue/A5uc=' cluster_0='kafka0:9093' cluster_1='kafka1:9094' groupID='ExposeAPIService'
kubectl delete pods -l io.kompose.service=expose-api



