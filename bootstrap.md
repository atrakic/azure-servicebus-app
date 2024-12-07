
## bootstrap

```
git init

dotnet new gitignore
dotnet new editorconfig

#dotnet new classlib -o src/Shared
#dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj reference src/Shared/Shared.csproj
#dotnet add src/Receiver.Worker/Receiver.Worker.csproj reference src/Shared/Shared.csproj

dotnet new console -o src/Submitter.ConsoleApp
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Azure.Identity
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Azure.Messaging.ServiceBus
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Microsoft.Extensions.DependencyInjection
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Microsoft.Extensions.Configuration.Json
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Microsoft.Extensions.Configuration

dotnet new console -o src/Receiver.Worker
dotnet add src/Receiver.Worker/Receiver.Worker.csproj package Azure.Identity
dotnet add src/Receiver.Worker/Receiver.Worker.csproj package Azure.Messaging.ServiceBus
dotnet add src/Receiver.Worker/Receiver.Worker.csproj package Microsoft.Extensions.DependencyInjection
dotnet add src/Receiver.Worker/Receiver.Worker.csproj package Microsoft.Extensions.Configuration.Json
dotnet add src/Receiver.Worker/Receiver.Worker.csproj package Microsoft.Extensions.Configuration

dotnet new xunit -o tests/UnitTests
dotnet add tests/UnitTests/UnitTests.csproj reference src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj
dotnet add tests/UnitTests/UnitTests.csproj reference src/Receiver.Worker/Receiver.Worker.csproj
dotnet add tests/UnitTests/UnitTests.csproj package NSubstitute
dotnet add tests/UnitTests/UnitTests.csproj package Bogus

dotnet new sln
dotnet sln add src/Receiver.Worker/Receiver.Worker.csproj
dotnet sln add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj
dotnet sln add tests/UnitTests/UnitTests.csproj
#dotnet sln add src/Shared/Shared.csproj

mkdir -p .github/workflows
curl -o .github/workflows/ci.yaml https://raw.githubusercontent.com/atrakic/MudBlazor.App/refs/heads/main/.github/workflows/ci.yaml

git add -A .
pre-commit run -a
git commit -m "Initial"
git remote add origin git@github.com:atrakic/azure-servicebus-app.git
git branch -M main
git push -u origin main

```
