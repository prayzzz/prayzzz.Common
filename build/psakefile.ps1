properties {
    Import-Module psake-contrib/teamcity.psm1
        
    # Project
    $config = Get-Value-Or-Default $env:CONFIGURATION "Release"
    $buildNumber = Get-Value-Or-Default $env:BUILD_NUMBER "1"

    $commonProjectDir = "src/prayzzz.Common" 
    $commonProjectFile = "src/prayzzz.Common/prayzzz.Common.csproj"
    $outputFolder = "dist/"

    # Version
    $version = "1.0.0-ci-$buildNumber"

    # Teamcity
    $isTeamcity = $env:TEAMCITY_VERSION
    if ($isTeamCity) { TeamCity-SetBuildNumber $version }

    # Change to root directory
    Set-Location "../"

    Write-Host "Configuration: $config"
    Write-Host "Version: $version"
}

FormatTaskName {
   ""
   ""
   Write-Host "Executing Task: $taskName" -foregroundcolor Cyan
}

# Alias

task Restore -depends Dotnet-Restore {
}

task Build -depends Dotnet-Build {
}

task Test -depends Dotnet-Test {
}

task Pack -depends Dotnet-Pack {
}

# Tasks

task Dotnet-Restore {
    exec { dotnet restore }
}

task Set-Version {
    Set-Version $commonProjectFile
}

task Dotnet-Build -depends Dotnet-Restore, Set-Version {
    exec { dotnet build --configuration $config }
}

task Dotnet-Test -depends Dotnet-Build {
}

task Dotnet-Pack -depends Dotnet-Test {
    Start-Pack $commonProjectFile
}

function Start-Pack($projectFile){
    exec { dotnet pack $projectFile --configuration $config --no-build --output ../../$outputFolder }

#    $file = $outputFolder + "$project.$version.nupkg"
#    exec { nuget push $file $env:NUGET_APIKEY -Source https://www.nuget.org/api/v2/package }
}

function Set-Version ($file) {        
    $xml = NEW-OBJECT XML

    Use-Object ($reader = [System.IO.StreamReader] $file)    {
        $xml.Load($reader)    
    }

    $xml.Project.PropertyGroup[0].Version = $version

    Use-Object ($writer = [System.IO.StreamWriter] $file) {
        $xml.Save($writer)
    }
}

function Start-Test ($project) {
    if ($isTeamCity) {
        TeamCity-TestSuiteStarted $project
    }

    exec { dotnet test "test/$project/" --configuration $config --result "TestResult.$project.xml" }

    if ($isTeamCity) {
        TeamCity-TestSuiteFinished $project
    }
}

function Get-Value-Or-Default($value, $default) {
    if (!$value -or $value -eq "") {
        return $default
    }

    return $value;
}

function Use-Object
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [AllowEmptyString()]
        [AllowEmptyCollection()]
        [AllowNull()]
        [Object]
        $InputObject,
 
        [Parameter(Mandatory = $true)]
        [scriptblock]
        $ScriptBlock
    )
 
    try
    {
        . $ScriptBlock
    }
    finally
    {
        if ($null -ne $InputObject -and $InputObject -is [System.IDisposable])
        {
            $InputObject.Dispose()
        }
    }
}