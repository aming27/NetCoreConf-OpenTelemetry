```bash
OPERATOR_INSTANCES="1" # >=2 in prod (they use leader election) 
```

# RabbitMQ Operator

```bash
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update

helm upgrade \
        --install \
        rabbitmq-operator \
        bitnami/rabbitmq-cluster-operator \
        -n rabbitmq-operator \
        --create-namespace \
        --set msgTopologyOperator.replicaCount=$OPERATOR_INSTANCES \
        --set clusterOperator.replicaCount=$OPERATOR_INSTANCES \
        --wait
```

> Note: Operator doesn't use pdb


# Prometheus Operator

TBD

# Demo

## Namespace

```bash
kubectl apply -f deploy/common
```

# OTEL Collector

```bash
helm repo add open-telemetry https://open-telemetry.github.io/opentelemetry-helm-charts
helm repo update

helm upgrade \
        --install \
        opentelemetry-collector \
        open-telemetry/opentelemetry-collector \
        -n otel-demo \
        --values deploy/otel/values.yaml \
        --wait
```

## RabbitMQ Cluster
<!-- https://github.com/rabbitmq/cluster-operator/tree/main/docs/examples -->
```bash
kubectl apply -f deploy/rabbitmq/cluster.yaml
```

## Prometheus server

```bash
kubectl apply -f deploy/prometheus/prometheus.yaml
```

## Zipkin
```bash
helm repo add openzipkin https://openzipkin.github.io/zipkin
helm repo update
helm upgrade \
        --install \
        zipkin \
        openzipkin/zipkin \
        -n otel-demo \
        --wait
```

## Apps

```bash
kubectl apply -f deploy/apps
```

```bash
# RabbitMQ Cluster
kubectl port-forward svc/rabbitmq-cluster -n otel-demo 15672:15672

# Prometheus 
kubectl port-forward svc/prometheus-operated -n otel-demo 9090:9090

# Zipkin 
kubectl port-forward svc/zipkin -n otel-demo 9411:9411

# Collector 
kubectl port-forward svc/opentelemetry-collector -n otel-demo 9888:9888 # Apps
kubectl port-forward svc/opentelemetry-collector -n otel-demo 8888:8888 # Collector

# Api 
kubectl port-forward svc/otel-api -n otel-demo 5000:80
```


# Clean up

```bash
kubectl delete -f deploy/apps
helm uninstall opentelemetry-collector -n otel-demo --wait
helm uninstall zipkin -n otel-demo --wait
kubectl delete -f deploy/prometheus/prometheus.yaml
kubectl delete -f deploy/rabbitmq/cluster.yaml
kubectl delete -f deploy/common
helm uninstall rabbitmq-operator -n rabbitmq-operator --wait
kubectl delete ns rabbitmq-operator
```