/*
 * MCL - Minecraft Launcher
 * Copyright (C) 2024 MCL Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */


using System.Collections.Generic;

namespace MCL.Core.Mapping;

public static class ObjectMapper
{
    /// <summary>
    /// Map properties of T to the properties of U.
    /// </summary>
#pragma warning disable IDE0079
#pragma warning disable S125
    /*
    * SourceObject source = new SourceObject
    * {
    *     Name = "John",
    *     Age = 30,
    *     Address = new Address
    *     {
    *         Street = "123 Main St",
    *         City = "New York"
    *     }
    * };
    * DestinationObject destination = new DestinationObject();
    * Dictionary<string, string> propertyMap = new Dictionary<string, string>
    * {
    *     { "Name", "FullName" },
    *     { "Age", "Years" },
    *     { "Address.Street", "Location.Street" },
    *     { "Address.City", "Location.City" }
    * };
    * ObjectMapper.MapProperties(source, destination, propertyMap);
    */
#pragma warning restore IDE0079, S125
    public static void MapProperties<T, U>(T source, U destination, Dictionary<string, string> propertyMap)
    {
        foreach ((string entryKey, string entryValue) in propertyMap)
        {
            if (typeof(T).GetProperty(entryKey) != null)
            {
                object? value = typeof(T).GetProperty(entryKey)?.GetValue(source);
                if (value != null && typeof(U).GetProperty(entryValue) != null)
                {
                    if (
                        value.GetType().IsClass
                        && !value.GetType().IsPrimitive
                        && !value.GetType().IsValueType
                        && value is not string
                    )
                        MapProperties(value, typeof(U).GetProperty(entryValue)?.GetValue(destination), propertyMap);
                    else
                        typeof(U).GetProperty(entryValue)?.SetValue(destination, value);
                }
            }
        }
    }
}
