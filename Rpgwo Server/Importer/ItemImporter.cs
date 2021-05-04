using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Rpgwo_Server.Importer
{
    public class ItemImporter
    {
        /*
        * Item.dat has a header of B9 (185 bytes).
        * First is a 4 byte int32 representing how many items are allocated. (Not all of it will be filled)
        * 
        * Afterwards, each item will have an entry of B5 (181 bytes).
        * 
        * So far, it seems to be ItemID, X, Y, Z all int16s from an initial glance.
        * 
        * At the of the day, I'm not too interested in grabbing all the fields.
        * I just need Item, Location, Locked, Movable, and ItemDatas.
        * For any Decay / Duration values, I can just reset them on import.
        * 
        * Also, Container mapping and Text mapping.
        */

        private readonly string _dataFolder;

        public ItemImporter(string datafolder)
        {
            _dataFolder = datafolder;
        }

        public void Import()
        {
            ReadItems(_dataFolder + "//item.dat");
        }

        /*
         * TODO :: 
         * ItemLock / ItemDegrade / ItemMove  / ItemReset
         */

        private void ReadItems(string itemdat)
        {
            int[] unknownBytes = new int[171];

            Dictionary<int, List<int>> tmp = new Dictionary<int, List<int>>();

            using (BinaryReader binary = new BinaryReader(new FileStream(itemdat, FileMode.Open)))
            {
                var itemCount = binary.ReadInt32();

                // Skip the rest of the header, since its unknown for now.
                binary.BaseStream.Position = 0xB9;

                for (int i = 0; i < itemCount; i++)
                {
                    var itemBasePos = binary.BaseStream.Position;
                    var nextItem = binary.BaseStream.Position + 0xB5; // 181 bytes per entry.

                    var itemId = binary.ReadInt16();

                    var x = binary.ReadInt16();
                    var y = binary.ReadInt16();
                    var z = binary.ReadInt16();

                    var unknown1 = binary.ReadInt32();

                    if (unknown1 != 0) { 
                        Console.WriteLine(String.Format("{0} : {1} : {2}", i, itemId, unknown1));

                        Console.WriteLine(String.Format("{0},{1},{2} ", x, y, z));
                    }

                    // Block of Unknowns
                    for (int j = 0; j < 3; j++)
                    {
                        var unknown = binary.ReadInt16();

                        if (unknown != 0)
                        {
                            if (j == 0)
                            {
                                //Console.WriteLine(String.Format("Item: {0} : Base {1}", itemId, itemBasePos));
                                //Console.WriteLine(String.Format("{0},{1},{2} has value {3} for unknown {4}", x, y, z, unknown, j));
                            }
                            
                        }
                    }

                    // This value nearly always seem to be 100 when it is set. Not sure what it means.
                    var unknown2 = binary.ReadInt16();
                    //

                    var quantity = binary.ReadInt32();

                    // Single Unknown
                    for (int j = 0; j < 1; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }
                    //

                    var degrade = binary.ReadInt16();

                    // Single Unknown
                    for (int j = 0; j < 1; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }
                    //

                    var data1 = binary.ReadInt16();
                    var data2 = binary.ReadInt16();
                    var data3 = binary.ReadInt16();
                    var data4 = binary.ReadInt16();
                    var data5 = binary.ReadInt32();
                    var data6 = binary.ReadInt16();
                    var data7 = binary.ReadInt32();
                    var data8 = binary.ReadInt32();
                    var data9 = binary.ReadInt32();

                    // Single Unknown
                    for (int j = 0; j < 1; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }

                    var ownerID = binary.ReadInt32();

                    // var itemLocked = binary.ReadInt16(); // Same spot as /itemgrade too?
                    // var itemMovable = binary.ReadInt16(); // Probably a bool, 0x0000 and 0xffff respectively.
                    for (int j = 0; j < 3; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }

                    var textID = binary.ReadInt16();

                    // 4 Unknown
                    for (int j = 0; j < 4; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }

                    var usesRemaining = binary.ReadInt16();

                    // Single Unknown
                    for (int j = 0; j < 1; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }

                    var creatorID = binary.ReadInt32();


                    for (int j = 0; j < 2; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }

                    var triggerID = binary.ReadInt16();

                    for (int j = 0; j < 11; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }

                    var weaponDurability = binary.ReadInt16();


                    for (int j = 0; j < 32; j++)
                    {
                        var unknown = binary.ReadInt16();
                    }

                    if (x == 194 && y == 76 && z == 0)
                    {
                        Console.WriteLine();
                    }

                    binary.BaseStream.Position = nextItem;
                }

                var bytesUsed = 0;
                for (var i = 0; i < unknownBytes.Length; i++)
                {
                    if (unknownBytes[i] != 0)
                        bytesUsed++;
                }

                Console.WriteLine();
            }
        }

        /*
        private void ReadItems(string itemdat)
        {
            int[] unknownBytes = new int[171];

            using (BinaryReader binary = new BinaryReader(new FileStream(itemdat, FileMode.Open)))
            {
                var itemCount = binary.ReadInt32();

                // Skip the rest of the header, since its unknown for now.
                binary.BaseStream.Position = 0xB9;

                for (int i = 0; i < itemCount; i++)
                {
                    var itemBasePos = binary.BaseStream.Position;
                    var nextItem = binary.BaseStream.Position + 0xB5; // 181 bytes per entry.

                    var itemId = binary.ReadInt16();

                    var x = binary.ReadInt16();
                    var y = binary.ReadInt16();
                    var z = binary.ReadInt16();

                    // 173 bytes remaining
                    for (int j = 0; j < 171; j++)
                    {
                        var b = binary.ReadByte();

                        if (b != 0)
                            unknownBytes[j]++;
                    }

                    //
                    binary.BaseStream.Position = itemBasePos + 0x12;
                    var unknown = binary.ReadInt16();
                    if (unknown != 100 && unknown != 0)
                        Console.WriteLine();
                    //
                    // binary.BaseStream.Position = itemBasePos + 0x14;
                    var quantity = binary.ReadInt32();

                    binary.BaseStream.Position = itemBasePos + 0x1A;
                    var degrade = binary.ReadInt16();

                    binary.BaseStream.Position = itemBasePos + 0x1E;
                    var data1 = binary.ReadInt16();
                    var data2 = binary.ReadInt16();
                    var data3 = binary.ReadInt16();
                    var data4 = binary.ReadInt16();
                    var data5 = binary.ReadInt32();
                    var data6 = binary.ReadInt16();
                    var data7 = binary.ReadInt32();
                    var data8 = binary.ReadInt32();
                    var data9 = binary.ReadInt32();
        
                    // Not entirely sure if this would be 16 or 32.
                    binary.BaseStream.Position = itemBasePos + 0x34;
                    var itemLocked = binary.ReadInt16(); // Same spot as /itemgrade too?
                    var itemMovable = binary.ReadInt16(); // Probably a bool, 0x0000 and 0xffff respectively.

                    binary.BaseStream.Position = itemBasePos + 0x3A;
                    var ownerID = binary.ReadInt32();


                    // Not entirely sure if this would be 16 or 32.
                    binary.BaseStream.Position = itemBasePos + 0x44;
                    var textID = binary.ReadInt16();

                    // Usages have a maximum int16 value.
                    binary.BaseStream.Position = itemBasePos + 0x4e;
                    var usesRemaining = binary.ReadInt16();

                    binary.BaseStream.Position = itemBasePos + 0x52;
                    var creatorID = binary.ReadInt32();

                    binary.BaseStream.Position = itemBasePos + 0x5A;
                    var triggerID = binary.ReadInt16();

                    binary.BaseStream.Position = itemBasePos + 0x72;
                    var weaponDurability = binary.ReadInt16();

                    if (x == 138 && y == 129 && z == 0)
                    {
                        Console.WriteLine();
                    }

                    binary.BaseStream.Position = nextItem;
                }

                var bytesUsed = 0;
                for (var i = 0; i < unknownBytes.Length; i++)
                {
                    if (unknownBytes[i] != 0)
                        bytesUsed++;
                }

                Console.WriteLine();
            }
        
        }*/
    }
}
