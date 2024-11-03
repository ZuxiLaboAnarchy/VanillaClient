// /*
//  *
//  * VanillaClient - ObjectKind.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Model
{
    /// <summary>
    /// Kind of an TOML object.
    /// </summary>
    public enum ObjectKind
    {
        Table,

        TableArray,

        Array,

        Boolean,

        String,

        Integer,

        Float,

        OffsetDateTime,

        LocalDateTime,

        LocalDate,

        LocalTime,
    }
}
