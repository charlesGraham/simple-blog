#!/bin/bash

API_PORT=5146  # Replace with the actual port number used by the API

cleanup() {
    # Find the PID of the dotnet run process on the specified port
    API_PID=$(lsof -ti :$API_PORT -sTCP:LISTEN)

    if [ -n "$API_PID" ]; then
        # Check if the command includes "dotnet"
        COMMAND=$(ps -o comm= -p $API_PID)
        if [[ $COMMAND == *"dotnet"* ]]; then
            echo "Terminating API process with PID $API_PID"
            kill -9 $API_PID
        else
            echo "Process on port $API_PORT is not recognized as the API."
        fi
    else
        echo "No API process found on port $API_PORT"
    fi
}

# Trap Ctrl + C and run the cleanup function
trap cleanup EXIT

# Navigate to the API folder and run the Web API
cd API
dotnet run &

# Navigate to the client folder and run the Angular app
cd ../client/SimpleBlog
npm start