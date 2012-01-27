NChanges
========

NChanges is a tool that detects and reports API changes in .NET assemblies.

NChanges.Core.dll contains all of the logic.

NChanges.Tool.exe is a command-line interface suitable for running from batch
files or continuous integration tools.

NChanges.GUI.exe is a graphical user interface that saves "project" files in a
fromat that's runnable by MSBuild. You can use it to get started and then tweak
it, but the GUI might not be able to read your modified MSBuild file so keep
that in mind.

The basic idea:

- Create snapshots of the different versions of your assemblies
- Generate a report of the differences
- Export the report as HTML or Excel

The snapshots are simple XML files that you can check into your source control
to avoid checking in binaries. If you don't mind checking in your binaries,
that's OK, you just have to generate the snapshots before generating your
reports.

    NChanges.Tool.exe snapshot C:\path\to\v1.0.0.0\assembly.dll
    NChanges.Tool.exe snapshot C:\path\to\v2.0.0.0\assembly.dll

Reports are also simple XML files, but they contain the differences between
snapshots. You need at least two snapshots to generate a report, but you can
include more to see the cumulative differences across versions.

    NChanges.Tool.exe report assembly-1.0.0.0-snapshot.xml assembly-2.0.0.0-snapshot.xml

If your snapshots are for different assemblies (you can even use wildcards) to
generate your reports, you'll get a different report file for each different
assembly.

Exporting the report as Excel is just running the Excel command on the XML
report file. You can specify more than one report file and it will combine all
of them into different worksheets in the generated Excel file.

    NChanges.Tool.exe excel assembly-2.0.0.0-report.xml

Each of the different commands has their own options. Run NChanges.Tool.exe
without any options to see the help.

If you don't want to do any of the above, use the GUI. If you have issues,
please file them on [GitHub](https://github.com/jdiamond/NChanges/issues).
