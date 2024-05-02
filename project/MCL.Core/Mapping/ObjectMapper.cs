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


using System;
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
    *    SourceObject source = new SourceObject { Name = "John", Age = 30 };
    *    DestinationObject destination = new DestinationObject();
    *
    *    // Define your property mappings using functions
    *    Dictionary<Func<SourceObject, object>, Action<DestinationObject, object>> propertyMap = new Dictionary<Func<SourceObject, object>, Action<DestinationObject, object>>
    *    {
    *        { src => src.Name, (dest, val) => dest.FullName = (string)val },
    *        { src => src.Age, (dest, val) => dest.Years = (int)val }
    *    };
    *
    *    // Map properties from source to destination
    *    ObjectMapper.MapProperties(source, destination, propertyMap);
    */
#pragma warning restore IDE0079, S125
    public static void MapProperties<T, U>(T source, U destination, Dictionary<Func<T, object>, Action<U, object>> propertyMap)
    {
        foreach ((Func<T, object> key, Action<U, object> value) in propertyMap)
        {
            object val = key.Invoke(source);
            value.Invoke(destination, val);
        }
    }
}
