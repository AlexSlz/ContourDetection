@echo off

:: Stage all changes
echo Staging changes...
git add .

:: Commit changes with a message
set /p commitMsg="Enter commit message: "
git commit -m "%commitMsg%"

:: Push changes to the remote repository
echo Pushing changes...
git push origin py

:: Done
echo All done!
pause