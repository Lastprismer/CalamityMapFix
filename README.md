**Just enable this when creating a new Calamity mod world.**
**Don't worry, It will NOT remove the astral biome chest in dungeon or change its position when the world is generating.**

This mod is useless after your world is generated and it doesn't make any changes to the game itself, so you can disable it after that.

This bug occurs because tmodloader changed the scope of variables which define the range of dungeon while the world is generating. These members (dMaxX, dMaxY, dMinX, dMinY) used to be private but now they are public. And then after tmodloader's update, Calamity can't get them by previous way of reflection, so the Astral Chest fail to generate and the process of world creating crashes.

What I done is just change the way in which the search for these members is conducted by reflection. And that's all. No more changes.
（by google translation)

****

开地图的时候加载一下就好，其他没啥了，星辉箱子是正常的

啥都没加，怕的话创建完世界关掉就好

这个bug是因为tml改了变量作用域，把地牢生成的范围从private改成了public，然后灾厄读取不到了就摆烂了
