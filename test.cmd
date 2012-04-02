NChanges.Tool\bin\Debug\NChanges.Tool.exe snapshot NChanges.Example1\bin\Debug\NChanges.Example.dll

NChanges.Tool\bin\Debug\NChanges.Tool.exe snapshot NChanges.Example2\bin\Debug\NChanges.Example.dll

NChanges.Tool\bin\Debug\NChanges.Tool.exe snapshot NChanges.Example3\bin\Debug\NChanges.Example.dll

NChanges.Tool\bin\Debug\NChanges.Tool.exe report NChanges.Example-?.0.0.0-snapshot.xml

NChanges.Tool\bin\Debug\NChanges.Tool.exe excel -n="\.(.+)$" NChanges.Example-3.0.0.0-report.xml

start NChanges.Example-3.0.0.0-report.xls
