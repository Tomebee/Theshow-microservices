# helm uninstall prometheus bitnami/kube-prometheus
# helm uninstall grafana bitnami/grafana

helm install prometheus bitnami/kube-prometheus

helm install grafana bitnami/grafana

echo "$(kubectl get secret grafana-admin --namespace default -o jsonpath="{.data.GF_SECURITY_ADMIN_PASSWORD}" | base64 --decode)"

export POD_NAME=$(kubectl get pods --namespace default -l "app.kubernetes.io/name=grafana,app.kubernetes.io/instance=grafana" -o jsonpath="{.items[0].metadata.name}")
kubectl --namespace default port-forward $POD_NAME 3000