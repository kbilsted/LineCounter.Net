# Before first run, set the Nuget API key in your NuGet.config to whatever is found on your profile on nuget.org
# For config file locations, see https://docs.microsoft.com/en-us/nuget/consume-packages/configuring-nuget-behavior#config-file-locations-and-uses
# If you have nuget on your path, you can use nuget config -Set ApiKey ^<your-api-key^>

# dotnet build
$p = pwd
dotnet pack --include-source --include-symbols -c Release -o "$p\nuget_package" 

cd nuget_package
dotnet nuget push *.symbols.nupkg --source https://nuget.org
# dotnet nuget push *.nupkg --source https://nuget.org
cd ..
rmdir -Force nuget_package
