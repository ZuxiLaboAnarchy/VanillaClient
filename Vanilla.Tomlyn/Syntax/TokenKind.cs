// /*
//  *
//  * VanillaClient - TokenKind.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Tomlyn.Syntax
{
    /// <summary>
    /// An enumeration to categorize tokens.
    /// </summary>
    public enum TokenKind
    {
        Invalid,

        Eof,

        Whitespaces,

        NewLine,

        Comment,

        OffsetDateTime,
        LocalDateTime,
        LocalDate,
        LocalTime,

        Integer,

        IntegerHexa,

        IntegerOctal,

        IntegerBinary,

        Float,

        String,

        StringMulti,

        StringLiteral,

        StringLiteralMulti,

        Comma,

        Dot,

        Equal,

        OpenBracket,
        OpenBracketDouble,
        CloseBracket,
        CloseBracketDouble,

        OpenBrace,
        CloseBrace,

        True,
        False,

        Infinite,
        PositiveInfinite,
        NegativeInfinite,

        Nan,
        PositiveNan,
        NegativeNan,

        BasicKey,
    }
}
