#!/usr/bin/env bash

echo $(bash --version 2>&1 | head -n 1)

#CUSTOMPARAM=0
BUILD_ARGUMENTS=()
for i in "$@"; do
    case $(echo $1 | awk '{print tolower($0)}') in
        # -custom-param) CUSTOMPARAM=1;;
        *) BUILD_ARGUMENTS+=("$1") ;;
    esac
    shift
done

set -eo pipefail
SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)

###########################################################################
# CONFIGURATION
###########################################################################

SOLUTION_DIRECTORY="$SCRIPT_DIR/src"
BUILD_PROJECT_FILE="$SCRIPT_DIR/build/_build.csproj"
BUILD_EXE_FILE="$SCRIPT_DIR/build/bin/Debug/_build.exe"
TEMP_DIRECTORY="$SCRIPT_DIR//.tmp"

NUGET_VERSION="latest"
NUGET_URL="https://dist.nuget.org/win-x86-commandline/$NUGET_VERSION/nuget.exe"

###########################################################################
# EXECUTION
###########################################################################

export NUGET_EXE="$TEMP_DIRECTORY/nuget.exe"
if [ ! -f "$NUGET_EXE" ]; then
    mkdir -p "$TEMP_DIRECTORY"
    curl -Lsfo "$NUGET_EXE" "$NUGET_URL"
elif [ "$NUGET_VERSION" == "latest" ]; then
    mono "$NUGET_EXE" update -Self
fi
echo $("$NUGET_EXE" help 2>&1 | head -n 1)

mono "$NUGET_EXE" restore "$BUILD_PROJECT_FILE" -SolutionDirectory "$SOLUTION_DIRECTORY"
msbuild "$BUILD_PROJECT_FILE"

mono "$BUILD_EXE_FILE" ${BUILD_ARGUMENTS[@]}
