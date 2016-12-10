properties {
    Import-Module psake-contrib/teamcity.psm1
        
    # Project
    $config = Get-Value-Or-Default $env:CONFIGURATION "Release"
    $buildNumber = Get-Value-Or-Default $env:BUILD_NUMBER "1"

    $commonProjectDir = "src/prayzzz.Common" 
    $commonProjectFile = "src/prayzzz.Common/prayzzz.Common.csproj"
    $mvcProjectDir = "src/prayzzz.Common.Mvc" 
    $mvcProjectFile = "src/prayzzz.Common.Mvc/prayzzz.Common.Mvc.csproj"
    $outputFolder = "dist/"

    # Version
    $version = "1.0.0-ci-$buildNumber"
	$gitCommit = (git rev-parse HEAD) | Out-String

    # Teamcity
    $isTeamcity = $env:TEAMCITY_VERSION
    if ($isTeamCity) { TeamCity-SetBuildNumber $version }

    # Change to root directory
    Set-Location "../"



    Write-Host "Configuration: $config"
    Write-Host "Version: $version"
	Write-Host "Commit: $gitCommit"
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
    Set-Version $mvcProjectFile
}

task Dotnet-Build -depends Dotnet-Restore, Set-Version {
    exec { dotnet build --configuration $config }
}

task Dotnet-Test -depends Dotnet-Build {
}

task Dotnet-Pack -depends Dotnet-Test {
    Start-Pack $commonProjectFile
    Start-Pack $mvcProjectFile
}

function Start-Pack($projectFile){
	Write-Host "Packing project $projectFile"
    exec { dotnet pack $projectFile --configuration $config --no-build --output ../../$outputFolder --include-source --include-symbols }
}

function Set-Version ($file) { 
	Write-Host "Setting version $version to $file"
	       
    $xml = NEW-OBJECT XML

    Use-Object ($reader = [System.IO.StreamReader] $file)    {
        $xml.Load($reader)    
    }

	Write-Host $xml.Project.PropertyGroup
    $xml.Project.PropertyGroup.Version = $version

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