git init

dotnet new console -o src/Submitter.ConsoleApp
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Azure.Identity
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Azure.Messaging.ServiceBus
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Microsoft.Extensions.DependencyInjection
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Microsoft.Extensions.Configuration.Json
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Microsoft.Extensions.Configuration

dotnet new console -o src/Receiver.ConsoleApp
dotnet add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj package Azure.Identity
dotnet add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj package Azure.Messaging.ServiceBus
dotnet add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj package Microsoft.Extensions.DependencyInjection
dotnet add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj package Microsoft.Extensions.Configuration.Json
dotnet add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj package Microsoft.Extensions.Configuration

dotnet new classlib -o src/ServiceBusLib
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj reference src/ServiceBusLib/ServiceBusLib.csproj
dotnet add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj reference src/ServiceBusLib/ServiceBusLib.csproj
dotnet sln add src/ServiceBusLib/ServiceBusLib.csproj

dotnet new xunit -o tests/UnitTests
dotnet add tests/UnitTests/UnitTests.csproj reference src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj
dotnet add tests/UnitTests/UnitTests.csproj reference src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj
dotnet add tests/UnitTests/UnitTests.csproj package NSubstitute
dotnet add tests/UnitTests/UnitTests.csproj package Bogus

dotnet new sln
dotnet sln add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj
dotnet sln add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj
dotnet sln add tests/UnitTests/UnitTests.csproj

mkdir -p .github/workflows
curl -o .github/workflows/ci.yaml https://raw.githubusercontent.com/atrakic/MudBlazor.App/refs/heads/main/.github/workflows/ci.yaml
dotnet new gitignore

git add -A .
dos2unix $(git ls-files)
git commit -m "Initial"
git remote add origin git@github.com:atrakic/azure-servicebus-app.git
git branch -M main
git push -u origin main
