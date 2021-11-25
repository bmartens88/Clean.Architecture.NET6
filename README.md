# Clean Architecture .NET 6 Web API Template Pack
## Installing and using the template
The template contained within this repository can be installed and used in multiple ways:
- Cloning the repository and installing the template directly from source
- Cloning the repository and using the template source directly
- Installing the template via NuGet

### Use template source code directly
Cloning the repository to use the template source directly is the least preferred and optimal way of using the template, as the user will have to do a lot of renaming him/her-self as well as copying source files to another directory etc, which might prove a challenge.

### Install template from source
Installing the template from source will add the template to the template overview of your `dotnet` SDK.

After cloning the repository, navigate to the root folder of the repository and use the following command, to add the template to your `dotnet` template list:
```powershell
dotnet new --install .\template\
```
If you run the command `dotnet new --list` you should now see the template added to the output:
```powershell
ASP.NET Core Web API Clean Architecture     webapicleanarch     [C#]   Web/WebAPI
```
You can now use this template to create a new project/solution using:
```powershell
dotnet new webapicleanarch -o <MY_PROJECT_NAME>
```