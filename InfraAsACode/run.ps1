& minikube -p minikube docker-env | Invoke-Expression

# helm install -f ./config/consul-config.yml consul hashicorp/consul

docker build -f ../TheShow.Core/Dockerfile -t theshow-core ./../
docker build -f ../TheShow.Client/Dockerfile -t theshow-react ./../TheShow.Client/.
docker build -f ../TheShow.Identity/Dockerfile -t theshow-identity ./../
docker build -f ../TheShow.Notifications/Dockerfile -t theshow-notifications ./../

kubectl apply -f ./deployments/apps-deploy.yml
kubectl apply -f ./services/apps-service.yml

kubectl apply -f ./config/ingress-config.yml

# kubectl apply -f https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml
kubectl apply -f ./deployments/infra-deploy.yml
kubectl apply -f ./services/infra-deploy.yml

#dashboard
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.4.0/aio/deploy/recommended.yaml
kubectl apply -f kusers.yml
minikube service -n kubernetes-dashboard --url kubernetes-dashboard

# minikube service consul-ui