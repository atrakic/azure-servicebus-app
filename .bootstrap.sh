git init
echo "# $(basename $PWD)"  >> .bootstrap.sh
mkdir -p .github/workflows
curl -o .github/workflows/ci.yaml https://raw.githubusercontent.com/atrakic/MudBlazor.App/refs/heads/main/.github/workflows/ci.yaml
dotnet new sln
dotnet new gitignore
dotnet new console -o src/Receiver.ConsoleApp
dotnet new console -o src/Submitter.ConsoleApp
dotnet new xunit -o tests/UnitTests
dotnet sln add tests/UnitTests/UnitTests.csproj
dotnet sln add src/Receiver.ConsoleApp/Receiver.ConsoleApp.csproj
dotnet sln add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Azure.Messaging.ServiceBus
dotnet add src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj package Azure.Messaging.ServiceBus
git add -A .
dos2unix $(git ls-files)
git commit -m "Initial"
git remote add origin git@github.com:atrakic/azure-servicebus-app.git
git branch -M main
git push -u origin main
