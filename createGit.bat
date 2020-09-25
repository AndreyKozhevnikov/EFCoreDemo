cd %~dp0
git init
timeout 1
git add .
timeout 1
git commit -m'init'
timeout 2
(goto) 2>nul & del "%~f0"

