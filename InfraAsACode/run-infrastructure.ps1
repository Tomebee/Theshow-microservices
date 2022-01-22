docker network create --attachable --driver overlay theshow_net
# docker stack deploy --compose-file .\Theshow-infrastucture-docker-stack.yml Theshow
docker run --restart always --name theshow_rabbitmq --hostname theshow_rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=sca -e RABBITMQ_DEFAULT_PASS=somekindofPass1. -v /docker-data/theshow_rabbitmq/data:/var/lib/rabbitmq -d rabbitmq:management
    