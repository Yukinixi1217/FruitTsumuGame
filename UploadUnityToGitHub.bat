@echo off
cd /d "%~dp0"

REM GitHubのURLはここで変更可能
set GIT_REPO=https://github.com/Yukinixi1217/FruitTsumuGame.git

REM .gitignoreをUnity用テンプレートで作成（上書きOK）
curl -L -o .gitignore https://raw.githubusercontent.com/github/gitignore/main/Unity.gitignore

REM Git 初期化とリモート設定
git init
git remote remove origin >nul 2>&1
git remote add origin %GIT_REPO%

REM ファイル追加 → コミット → push
git add .
git commit -m "Initial commit with .gitignore"
git branch -M main
git push -u origin main

pause
