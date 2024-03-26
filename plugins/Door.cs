//This is an example plugin source!
using System;
using System.Collections.Generic;
using BlockID = System.UInt16;
using MCGalaxy.Events.ServerEvents;
using MCGalaxy.Events;
using MCGalaxy.Events.LevelEvents;
using MCGalaxy.Events.PlayerEvents;
using MCGalaxy.Tasks;
using MCGalaxy.Maths;
namespace MCGalaxy {
	public class DoorBlock {
		public BlockID Item_Block        {get; set;}
		public BlockID Top_Block         {get; set;}
		public BlockID Top_Block_Open    {get; set;}
		public BlockID Bottom_Block      {get; set;}
		public BlockID Bottom_Block_Open {get; set;}
		public BlockID Top_Block_Inverse {get; set;}
		public BlockID Top_Block_Inverse_Open {get; set;}
		public BlockID Bottom_Block_Inverse {get; set;}
		public BlockID Bottom_Block_Inverse_Open {get; set;}
	}
	public class DoorConfig {
		public BlockID BLOCK_ITEM_ID 		{get; set;}
		public string  BLOCK_ITEM_NAME 		{get; set;}
		public ushort  TEXTURE_ITEM 		{get; set;}
		public ushort  TEXTURE_BLOCK_TOP    {get; set;}
		public ushort  TEXTURE_BLOCK_BOTTOM {get; set;}
	}
	public class Door : Plugin {
		public override string name { get { return "Door"; } }
		public override string MCGalaxy_Version { get { return "1.9.1.2"; } }
		public override int build { get { return 100; } }
		public override string welcome { get { return "Loaded Message!"; } }
		public override string creator { get { return "morgana"; } }
		public override bool LoadAtStartup { get { return true; } }

		bool ToggleOnBreak = true;

		public List<DoorConfig> DoorConfigs = new List<DoorConfig>()
		{
			new DoorConfig() // Just add more of these if you want more doors! (Make sure you have a unique id, that has 8 further free Ids after it)
			{
				BLOCK_ITEM_ID = 66, // Takes 9 slots,  So Block ids 66 to 74 will be reserved for the door, keep in mind when adding new doors!
				BLOCK_ITEM_NAME = "Wooden Door",
				TEXTURE_ITEM = 182, // Press F10 to see texture Ids
				TEXTURE_BLOCK_TOP = 183, // Press F10 to see texture Ids
				TEXTURE_BLOCK_BOTTOM = 184 // Press F10 to see texture Ids
			},
			new DoorConfig() // Iron Door
			{
				BLOCK_ITEM_ID = 75, // Takes 9 slots,  So Block ids 75 to 83 will be reserved for the door, keep in mind when adding new doors!
				BLOCK_ITEM_NAME = "Iron Door",
				TEXTURE_ITEM = 185, // Press F10 to see texture Ids
				TEXTURE_BLOCK_TOP = 186, // Press F10 to see texture Ids
				TEXTURE_BLOCK_BOTTOM = 187 // Press F10 to see texture Ids
			},
			new DoorConfig() // Dark Oak Door
			{
				BLOCK_ITEM_ID = 84, // Takes 9 slots,  So Block ids 84 to 95 will be reserved for the door, keep in mind when adding new doors!
				BLOCK_ITEM_NAME = "Dark Oak Door",
				TEXTURE_ITEM = 188, // Press F10 to see texture Ids
				TEXTURE_BLOCK_TOP = 189, // Press F10 to see texture Ids
				TEXTURE_BLOCK_BOTTOM = 190 // Press F10 to see texture Ids
			},
		};
		
		
		
		
		
		
		
		public override void Load(bool startup) {
			//LOAD YOUR PLUGIN WITH EVENTS OR OTHER THINGS!
			OnBlockChangingEvent.Register(HandleBlockChanged, Priority.Low);
			foreach (var d in DoorConfigs)
			{
				LoadDoor(d);
			}
		}
                        
		public override void Unload(bool shutdown) {
			//UNLOAD YOUR PLUGIN BY SAVING FILES OR DISPOSING OBJECTS!
			OnBlockChangingEvent.Unregister(HandleBlockChanged);
		}
                        
		public override void Help(Player p) {
			//HELP INFO!
		}
		
		public void AddBlock(BlockDefinition def)
		{
			BlockDefinition.Add(def, BlockDefinition.GlobalDefs, null );
		}
		public void AddBlockItem(ushort Id, string Name, ushort Texture)
		{
			BlockDefinition def = new BlockDefinition();
				def.RawID = Id; def.Name = Name;
				def.Speed = 1; def.CollideType = 0;
				def.TopTex = Texture; def.BottomTex = Texture;
				
				def.BlocksLight = false; def.WalkSound = 1;
				def.FullBright = false; def.Shape = 0;
				def.BlockDraw = 2; def.FallBack = 5;
				
				def.FogDensity = 0;
				def.FogR = 0; def.FogG = 0; def.FogB = 0;
				def.MinX = 0; def.MinY = 0; def.MinZ = 0;
				def.MaxX = 0; def.MaxY = 0; def.MaxZ = 0;
				
				def.LeftTex = Texture; def.RightTex = Texture;
				def.FrontTex = Texture; def.BackTex = Texture;
				def.InventoryOrder = -1;
			AddBlock(def);
		}
		public void AddDoorBlock(ushort Id, ushort MinX, ushort MinY, ushort MinZ, ushort MaxX, ushort MaxY, ushort MaxZ, ushort TEXTURE_SIDE, ushort TEXTURE_FRONT, bool Transperant)
		{
				ushort RawID = Id;
				string Name = "";
				byte Speed = 1;
				byte CollideType = 2;
				ushort TopTex = TEXTURE_SIDE;
				ushort BottomTex = TEXTURE_SIDE;
				bool BlocksLight = false;
				byte WalkSound = 1;
				bool FullBright = false;
				byte Shape = 16;
				byte BlockDraw =  (byte)(Transperant ? 1 : 0);
				byte FallBack = 5;
				byte FogDensity = 0;
				byte FogR = 0;
				byte FogG = 0;
				byte FogB = 0;
				ushort LeftTex = TEXTURE_FRONT;
				ushort RightTex = TEXTURE_FRONT;
				ushort FrontTex = TEXTURE_FRONT;
				ushort BackTex = TEXTURE_FRONT;
				int InventoryOrder = -1;
				BlockDefinition def = new BlockDefinition();
				def.RawID = RawID; def.Name = Name;
				def.Speed = Speed; def.CollideType = CollideType;
				def.TopTex = TopTex; def.BottomTex = BottomTex;
				
				def.BlocksLight = BlocksLight; def.WalkSound = WalkSound;
				def.FullBright = FullBright; def.Shape = Shape;
				def.BlockDraw = BlockDraw; def.FallBack = FallBack;
				
				def.FogDensity = FogDensity;
				def.FogR = FogR; def.FogG = FogG; def.FogB = FogB;
				def.MinX = (byte)MinX; def.MinY = (byte)MinY; def.MinZ = (byte)MinZ;
				def.MaxX = (byte)MaxX; def.MaxY = (byte)MaxY; def.MaxZ = (byte)MaxZ;
				
				def.LeftTex = LeftTex; def.RightTex = RightTex;
				def.FrontTex = FrontTex; def.BackTex = BackTex;
				def.InventoryOrder = InventoryOrder;
				AddBlock(def);
		}
		public void AddDoorBlocks(DoorBlock door, string BLOCK_ITEM_NAME, ushort TEXTURE_ITEM, ushort TEXTURE_BLOCK_BOTTOM, ushort TEXTURE_BLOCK_TOP)
		{
				AddBlockItem(door.Item_Block, BLOCK_ITEM_NAME, TEXTURE_ITEM);
				

				AddDoorBlock(door.Bottom_Block,		 		 0, 0, 0,   4, 16, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_BOTTOM, false);
				AddDoorBlock(door.Bottom_Block_Open, 		 0, 0, 0,   16, 4, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_BOTTOM, false);
				AddDoorBlock(door.Bottom_Block_Inverse,	 	 12, 0, 0,   16, 16, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_BOTTOM, false);
				AddDoorBlock(door.Bottom_Block_Inverse_Open, 0, 12, 0,   16, 16, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_BOTTOM, false);
				
				AddDoorBlock(door.Top_Block, 				0,0,0, 4, 16, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_TOP, true);
				AddDoorBlock(door.Top_Block_Open, 			0,0,0, 16, 4, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_TOP, true);
				AddDoorBlock(door.Top_Block_Inverse, 		12, 0, 0,   16, 16, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_TOP, true);
				AddDoorBlock(door.Top_Block_Inverse_Open, 	0, 12, 0,   16, 16, 16, TEXTURE_BLOCK_BOTTOM, TEXTURE_BLOCK_TOP, true);
				
				
				door.Item_Block 		       = (ushort)(door.Item_Block + 256					);
				door.Bottom_Block_Open         = (ushort)(door.Bottom_Block_Open + 256			);
				door.Bottom_Block 		  	   = (ushort)(door.Bottom_Block + 256				);
				door.Bottom_Block_Inverse	   = (ushort)(door.Bottom_Block_Inverse + 256		);
				door.Bottom_Block_Inverse_Open = (ushort)(door.Bottom_Block_Inverse_Open + 256	);
				
				door.Top_Block 					= (ushort)(door.Top_Block + 256						);
				door.Top_Block_Open 			= (ushort)(door.Top_Block_Open + 256				);
				door.Top_Block_Inverse 			= (ushort)(door.Top_Block_Inverse + 256				);
				door.Top_Block_Inverse_Open 	= (ushort)(door.Top_Block_Inverse_Open + 256		);
				
				// (0 0 0) (4 16 16)
				// (0 0 0) (16 16 4)
				// (4 0 0) (4 16 16)?
				// (4 0 0) (16 16 4)?

		}
		public void LoadDoor(DoorConfig config)
		{
			DoorBlock newDoor = new DoorBlock(){
				Item_Block = config.BLOCK_ITEM_ID,
				Top_Block = (ushort)(config.BLOCK_ITEM_ID + 1),
				Top_Block_Open = (ushort)(config.BLOCK_ITEM_ID + 2),
				Bottom_Block = (ushort)(config.BLOCK_ITEM_ID + 3),
				Bottom_Block_Open = (ushort)(config.BLOCK_ITEM_ID + 4),
				Top_Block_Inverse = (ushort)(config.BLOCK_ITEM_ID + 5),
				Top_Block_Inverse_Open = (ushort)(config.BLOCK_ITEM_ID + 6),
				Bottom_Block_Inverse = (ushort)(config.BLOCK_ITEM_ID + 7),
				Bottom_Block_Inverse_Open = (ushort)(config.BLOCK_ITEM_ID + 8),
			};
			AddDoorBlocks(newDoor, config.BLOCK_ITEM_NAME, config.TEXTURE_ITEM, config.TEXTURE_BLOCK_BOTTOM, config.TEXTURE_BLOCK_TOP);
			DoorTypes.Add(newDoor);
		}
	
		public List<DoorBlock> DoorTypes = new List<DoorBlock>(){};
		public bool IsDoor(BlockID block)
		{
			var b = block;
			DoorBlock d = GetDoorFromBlock(b);
			if ( d == null)
			{
				return false;
			}
			return true;
		}
		public bool IsDoorItem(BlockID block)
		{
			var b = block;
			
			DoorBlock d = GetDoorFromItem(b);
			if ( d == null)
			{
				return false;
			}
			return true;
		}
		public DoorBlock GetDoorFromItem(BlockID block)
		{
			foreach (DoorBlock door in DoorTypes)
			{
				if (door.Item_Block == block)
				{
					return door;
				}
			}
			return null;
		}
		public DoorBlock GetDoorFromBlock(BlockID block)
		{
			foreach (DoorBlock door in DoorTypes)
			{
				if (door.Bottom_Block == block)
				{
					return door;
				}
				if (door.Bottom_Block_Open == block)
				{
					return door;
				}
				if (door.Top_Block == block)
				{
					return door;
				}
				if (door.Top_Block_Open == block)
				{
					return door;
				}
				if (door.Top_Block_Inverse == block)
				{
					return door;
				}
				if (door.Top_Block_Inverse_Open == block)
				{
					return door;
				}
				if (door.Bottom_Block_Inverse == block)
				{
					return door;
				}
				if (door.Bottom_Block_Inverse_Open == block)
				{
					return door;
				}
			}
			return null;
		}
		public bool IsDoor(Level level, ushort x, ushort y, ushort z)
		{
			var b = level.FastGetBlock((ushort)x, (ushort)y, (ushort)z);
			
			return IsDoor(b);
		}
		public bool IsDoorBottom(Level level, ushort x, ushort y, ushort z)
		{
			var b = level.FastGetBlock((ushort)x, (ushort)y, (ushort)z);
			DoorBlock d = GetDoorFromBlock(b);
			if ( d == null)
			{
				return false;
			}
			return ( (b == d.Bottom_Block) || (b == d.Bottom_Block_Open) || (b == d.Bottom_Block_Inverse) || (b == d.Bottom_Block_Inverse_Open));
		}
	
		public void OpenDoor(Level level, ushort x, ushort y, ushort z)
		{
			BlockID b = level.FastGetBlock((ushort)x, (ushort)(y), (ushort)z);
			DoorBlock d = GetDoorFromBlock(b);
			if ( d == null)
			{
				return;
			}
			int offset_y = 0;
			if (!IsDoorBottom(level, x, y, z))
			{
				offset_y = -1;
			}
			ushort result_bottom = (b == d.Top_Block_Inverse || b == d.Bottom_Block_Inverse) ? d.Bottom_Block_Inverse_Open : d.Bottom_Block_Open;
			ushort result_top    = (b == d.Top_Block_Inverse || b == d.Bottom_Block_Inverse) ? d.Top_Block_Inverse_Open    : d.Top_Block_Open;
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y    ), z, result_bottom);
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y + 1), z, result_top   );	
			
		}
		public void CloseDoor(Level level, ushort x, ushort y, ushort z)
		{
			BlockID b = level.FastGetBlock((ushort)x, (ushort)(y), (ushort)z);
			DoorBlock d = GetDoorFromBlock(b);
			if ( d == null)
			{
				return;
			}
			int offset_y = 0;
			if (!IsDoorBottom(level, x, y, z))
			{
				offset_y = -1;
			}
			ushort result_bottom = (b == d.Top_Block_Inverse_Open || b == d.Bottom_Block_Inverse_Open) ? d.Bottom_Block_Inverse : d.Bottom_Block;
			ushort result_top    = (b == d.Top_Block_Inverse_Open || b == d.Bottom_Block_Inverse_Open) ? d.Top_Block_Inverse    : d.Top_Block;
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y    ), z, result_bottom);
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y + 1), z, result_top   );	
		}
		public void ToggleDoor(Level level, ushort x, ushort y, ushort z)
		{
			BlockID b = level.FastGetBlock((ushort)x, (ushort)(y), (ushort)z);
			DoorBlock d = GetDoorFromBlock(b);
			if ( d == null)
			{
				return;
			}
			if ( b == d.Top_Block || b == d.Bottom_Block || b == d.Top_Block_Inverse || b == d.Bottom_Block_Inverse)
			{
				OpenDoor( level, x, y, z);
			}
			else if (b == d.Top_Block_Open || b == d.Bottom_Block_Open || b == d.Bottom_Block_Inverse_Open || b == d.Top_Block_Inverse_Open)
			{
				CloseDoor(level, x, y, z);
			}
		}
		
		void PlaceDoor(Player p, ushort x, ushort y, ushort z, BlockID block)
		{
			if (!IsDoorItem(block))
			{
				return;
			}
			if ( y > 0 && p.level.FastGetBlock((ushort)x, (ushort)(y-1), (ushort)z) == 0) // if placing above air
			{
				return;
			}
			DoorBlock d = GetDoorFromItem(block);
			
			bool Inverse = ( ((p.Pos.X /32 ) > x) || ((p.Pos.Z /32 ) > z) );
			bool Open = (p.Pos.Z /32 ) < z || (p.Pos.Z /32 ) > z;
			p.level.UpdateBlock(Player.Console, (ushort)x, (ushort)(y  ), (ushort)z, ( Inverse ? d.Bottom_Block_Inverse : d.Bottom_Block));
			p.level.UpdateBlock(Player.Console, (ushort)x, (ushort)(y+1), (ushort)z, ( Inverse ? d.Top_Block_Inverse    : d.Top_Block   ));
			
			if (Open)
			{
				OpenDoor(p.level, x, y, z);
			}
		
		}
		
		void HandleBlockChanged(Player p, ushort x, ushort y, ushort z, BlockID block, bool placing, ref bool cancel)
        {
			if (!ToggleOnBreak)
			{
				cancel = CheckDoorOnPlace(p, x, y, z, block, placing, cancel);
			}
			else
			{
				cancel = CheckDoorOnBreak(p, x, y, z, block, placing, cancel);
			}
			CheckShouldDoorBreak(p, x, y, z, block, placing);
        }
		void CheckShouldDoorBreak(Player p, ushort x, ushort y, ushort z, BlockID block, bool placing)
		{
			if (!(block == 0 || !placing))
			{
				return;
			}
			BlockID b = p.level.FastGetBlock((ushort)x, (ushort)(y+1), (ushort)z);
			DoorBlock d = GetDoorFromBlock(b);
			if (d == null)
			{
				return;
			}
			if (! ( b == d.Bottom_Block || b == d.Bottom_Block_Open || b == d.Bottom_Block_Inverse || b == d.Bottom_Block_Inverse_Open))
			{
				return;
			}
			p.level.UpdateBlock(Player.Console, x, (ushort)(y+1), z, 0);
			p.level.UpdateBlock(Player.Console, x, (ushort)(y+2), z, 0);
		}
		bool CheckDoorOnBreak(Player p, ushort x, ushort y, ushort z, BlockID block, bool placing, bool cancel)
		{
			if (placing)
			{
				if (IsDoorItem(block))
				{
					PlaceDoor(p, x, y, z, block);
					//cancel = true;
					p.RevertBlock(x, y, z);
					return true;
				}
				return cancel;
			}
			if (IsDoor(p.level,x,y,z))
			{
				p.RevertBlock(x, y, z);
				ToggleDoor(p.level, x, y, z);
				return true;
			}
			return cancel;
		}
		bool CheckDoorOnPlace(Player p, ushort x, ushort y, ushort z, BlockID block, bool placing, bool cancel)
		{
			if (!placing)
			{
				return cancel;
			}
			Vec3F32 dir = DirUtils.GetDirVector(p.Rot.RotY, p.Rot.HeadX);
	        ushort nx = (ushort)Math.Round(p.Pos.BlockX + dir.X);
            ushort ny = (ushort)Math.Round(p.Pos.BlockY + dir.Y);
            ushort nz = (ushort)Math.Round(p.Pos.BlockZ + dir.Z);
			for (int i = 0; i < 5; i++)
			{
				nx = (ushort)Math.Round(p.Pos.BlockX + (dir.X * i));
				ny = (ushort)Math.Round(p.Pos.BlockY + (dir.Y * i));
				nz = (ushort)Math.Round(p.Pos.BlockZ + (dir.Z * i));
				//p.Message(p.level.FastGetBlock((ushort)nx, (ushort)(ny), (ushort)nz).ToString());
				if (!IsDoor(p.level,nx,ny,nz))
				{
					if (i == 4)
					{
						if (IsDoorItem(block))
						{
							PlaceDoor(p, x, y, z, block);
							//cancel = true;
							p.RevertBlock(x, y, z);
							return true;
						}
						return cancel;
					}
				}
				else
				{
					break;
				}
			}
			//cancel = true;
			p.RevertBlock(x, y, z);
			ToggleDoor(p.level, nx, ny, nz);
			return true;
		}
	}
}