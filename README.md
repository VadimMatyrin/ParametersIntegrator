# ParametersIntegrator

Tool to replace or edit parameters values in config json deployment files

To use:
1) Create sections.txt file in the same folder as the project
2) Fill sections.txt with this - 
{
    "value": [
    ]
}

3) Add all required updated params in "value" array
4) Launch an app
5) Enter folder path to search for json files
6) Enter pattern to search for in "name" values of appsettings. Matching values will be deleted and the last one will be replaced with the first new value from sections.txt file.
All remaining new sections will be placed after it. If no matching values are found new values will be added at the end

Note: 
Tool also formatts all of the files that it edits. Formatting is done using spaces or tabs, based on the initial file formatting
