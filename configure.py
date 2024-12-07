import json
import os
import subprocess


def get_bicep_outputs(bicep_file):
    # result = subprocess.run(['az', 'deployment',
    #                         'group', 'create', '--template-file', bicep_file,
    #                         '--query', 'properties.outputs'],
    #                         capture_output=True, text=True)

    # hardcoding the outputs for now
    result = {
        "ServiceBus": {
            "Namespace": "your-servicebus-namespace",
            "Topic": "your-topic-name",
            "Subscription": "your-subscription-name",
        }
    }
    return result


def update_appsettings(cproj_file, outputs):
    appsettings_path = os.path.join(os.path.dirname(cproj_file), "appsettings.json")
    print(f"Updating appsettings for: {cproj_file}")
    if os.path.exists(appsettings_path):
        with open(appsettings_path, "w") as f:
            json.dump(outputs, f, indent=4)
        return


def main():
    bicep_file = "infra/main.bicep"
    outputs = get_bicep_outputs(bicep_file)

    cproj_files = [
        "src/Receiver.Worker/Receiver.Worker.csproj",
        "src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj",
    ]
    for cproj_file in cproj_files:
        update_appsettings(cproj_file, outputs)


if __name__ == "__main__":
    main()
