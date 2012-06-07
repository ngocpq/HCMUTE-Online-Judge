
echo BEGIN
Bin\cl.exe  file.cpp /IInclude  /FoFile.obj  /FeFile.exe /link/LIBPATH:Lib
Echo END
pause