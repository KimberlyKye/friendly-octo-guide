#!/bin/bash
dotnet publish -c Release -o ./output
dotnet ./output/YourProject.dll
