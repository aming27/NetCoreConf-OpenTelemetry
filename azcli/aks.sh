#resourcegroup
az group create --name "NetCoreConfBcn2023" --location "West Europe"

#Create aks
az aks create -g "NetCoreConfBcn2023" -n "AKSClusterTest" --enable-managed-identity --node-count 2 --enable-addons monitoring --enable-msi-auth-for-monitoring  --generate-ssh-keys

#Az get Credentials
az aks get-credentials --resource-group "NetCoreConfBcn2023" --name "AKSClusterTest"

