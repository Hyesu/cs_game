# `create design_data --name <name>` Command

This document outlines the steps performed by the `create design_data --name <name>` command.

1.  **Use `<name>` as the base name.**

2.  **Create Table Class:** Read `GeminiCli/ClassTemplates/DesignTable.cs` and replace `___` with `<name>` to create `Assets/Scripts/DesignTable/Table/D<name>Table.cs`.

3.  **Create Entry Class:** Read `GeminiCli/ClassTemplates/DesignEntry.cs` and replace `___` with `<name>` to create `Assets/Scripts/DesignTable/Entry/D<name>.cs`.

4.  **Find or Create XML:** Look for `Assets/HData/DesignData/<name>/<name>.xml`. If it doesn't exist, create it with placeholder content.

5.  **Populate Entry Class:** Modify the `D<name>.cs` file to include `public readonly` fields and constructor parsing logic for the attributes found in the XML file.
    *   **Important:** Do **not** include fields for `Id` or `StrId`. These are handled by the base `DEntry` class.
    *   For all other attributes, infer the type (e.g., `int` for `Episode`, `string` for others).
    *   Use the correct, strongly-typed methods from the `IdParsedObject` interface (defined in `Assets/Scripts/DesignTable/Core/IdParsedObject.cs`). For example, use `parsedObject.GetInt("AttributeName")` for integers and `parsedObject.GetString("AttributeName")` for strings.

6.  **Update DContext:** Modify `Assets/Scripts/DesignTable/Core/DContext.cs`:
    *   In the `// indexes` section, add the line: `public readonly D<name>Table <name>;`
    *   In the `DContext` constructor, inside the `// create indexes` block, add the line: `<name> = Add(new D<name>Table("<name>", xmlParser));`
    *   **Important:** Do not add any extra comments like `// Added for <name>`.