//
//  PackFile.cs
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Andrés Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Pack
{
    public class PackFile
    {
        byte[] Magic = { 0x50, 0x41, 0x43, 0x4b, 0xff };
        FileStream fs;
        DeflateStream ds;
        BinaryReader br;

        Directory Root;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PackFile"/>.
        /// </summary>
        public PackFile(string path)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            ds = new DeflateStream(fs, CompressionMode.Decompress);
            br = new BinaryReader(ds);
            if (!Magic.SequenceEqual(br.ReadBytes(Magic.Length)))
                throw new Exception("Not a PACK file.");

            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Corrupt PACK file.", ex);
            }
        }

        public Node ReadNode()
        {
            Node ret = new Node();

            ret.Type = br.ReadByte();
            ret.length = br.ReadInt32();
            ret.name = br.ReadString();
            switch (ret.Type)
            {
                case 0:

                    break;
                default:
                    break;
            }

            return ret;
        }
    }

    public class Node
    {
        public int offset;
        public byte Type;
        public int length;
        public string name;

    }

    public class Directory : Node
    {
        public Dictionary<string, Directory> SubDirs;
        public Dictionary<string, File> Files;
    }

    public class File : Node
    {

    }
}
