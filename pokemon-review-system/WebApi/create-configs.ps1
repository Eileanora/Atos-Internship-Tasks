# Define the paths
$modelsFolder = ".\Models"
$outputFolder = ".\Configurations"

# Create output folder if it doesn't exist
if (-not (Test-Path -Path $outputFolder)) {
    New-Item -ItemType Directory -Path $outputFolder | Out-Null
    Write-Host "Created directory: $outputFolder"
}

# Get all files in the Models folder (excluding extensions)
$modelFiles = Get-ChildItem -Path $modelsFolder -File | ForEach-Object {
    [System.IO.Path]::GetFileNameWithoutExtension($_.Name)
}

# Loop through each model file and create the configuration file
foreach ($model in $modelFiles) {
    $configFileName = "${model}Configurations.cs"
    $filePath = Join-Path -Path $outputFolder -ChildPath $configFileName
    
    # Create the file content
    $fileContent = @"
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Configurations;

public class ${model}Configurations : IEntityTypeConfiguration<${model}>
{
    public void Configure(EntityTypeBuilder<${model}> builder)
    {
        throw new NotImplementedException();
    }
}
"@

    # Write the content to the file
    Set-Content -Path $filePath -Value $fileContent
    
    Write-Host "Created configuration file: $filePath"
}

Write-Host "Configuration files created successfully!"