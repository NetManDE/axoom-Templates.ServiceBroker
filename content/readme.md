# My Service Broker

This repository contains the source code for My Service Broker.

my description

## Development

Run `build.ps1` to compile the source code and package the result in Docker images. This script takes a version number as an input argument. The source code itself contains no version numbers. Instead version numbers should be determined at build time using [GitVersion](http://gitversion.readthedocs.io/).

Configuration overrides for local development are specified in:
- [launchSettings.json](src/ServiceBroker/Properties/launchSettings.json) for IDEs
- [docker-compose.override.yml](src/docker-compose.override.yml) for Docker Compose

To build and then run locally with Docker Compose:
```powershell
cd src
./build-dotnet.ps1
docker-compose up --build
```
You can then interact with the API at: http://localhost:12345/swagger/

The credentials for local test instances are username `test` and password `test`.

To access Prometheus metrics locally without Docker Compose run:
```powershell
netsh http add urlacl http://*:5000/ user=$env:USERDOMAIN\$env:USERNAME
```
You can then access the metrics at: http://localhost:5000/

## Deployment

For deployment in a Kubernetes cluster, see the [Helm Chart readme](charts/myvendor-servicebroker/README.md).

The credentials are automatically generated at deployment time.

Once the Broker is deployed in the cluster you should be able to see the services it provides by running:
```
kubectl get clusterserviceclass
```
