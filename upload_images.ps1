$dockerComposeFile = ".\docker-compose-clustercount2-partitioncount2.yml"
$dockerHubUsername = "mircoantona"
$dockerRepository = "weathereventnotifier"
docker login -u $dockerHubUsername
docker-compose -f $dockerComposeFile -p $dockerRepository build

$services = (docker-compose -f $dockerComposeFile config --services).split(" ")
foreach ($service in $services) {
    $taggedImage = "$dockerHubUsername/$dockerRepository/$service:latest"  
    $ToPushImage = "${dockerHubUsername}/${dockerRepository}:${service}"
    docker tag "weathereventnotifier-$service" "$ToPushImage"
    docker push "$ToPushImage"
}

docker logout
