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
		public BlockID Bottom_Block      {get; set;}
		public BlockID Top_Block_Open    {get; set;}
		public BlockID Bottom_Block_Open {get; set;}
	}
	public class Door : Plugin {
		public override string name { get { return "Door"; } }
		public override string MCGalaxy_Version { get { return "1.9.1.2"; } }
		public override int build { get { return 100; } }
		public override string welcome { get { return "Loaded Message!"; } }
		public override string creator { get { return "morgana"; } }
		public override bool LoadAtStartup { get { return true; } }

		bool ToggleOnBreak = true;
		public override void Load(bool startup) {
			//LOAD YOUR PLUGIN WITH EVENTS OR OTHER THINGS!
			OnBlockChangingEvent.Register(HandleBlockChanged, Priority.Low);
			
		}
                        
		public override void Unload(bool shutdown) {
			//UNLOAD YOUR PLUGIN BY SAVING FILES OR DISPOSING OBJECTS!
			OnBlockChangingEvent.Unregister(HandleBlockChanged);
		}
                        
		public override void Help(Player p) {
			//HELP INFO!
		}
		

		
		public List<DoorBlock> DoorTypes = new List<DoorBlock>()
		{
			new DoorBlock() // Wooden
			{
				Item_Block = 256 + 66, // t id 182
				Top_Block = 256 + 69,
				Bottom_Block = 256 + 67,
				Top_Block_Open = 256 + 70,
				Bottom_Block_Open = 256 + 68
				
			},
			/*new DoorBlock() // test
			{
				Item_Block = 6, // t id 182
				Top_Block = 22,
				Bottom_Block = 24,
				Top_Block_Open = 23,
				Bottom_Block_Open = 25
				
			}*/
		};
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
			return ( (b == d.Bottom_Block) || (b == d.Bottom_Block_Open));
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
			if (!((b == d.Bottom_Block) || (b == d.Bottom_Block_Open)))
			{
				offset_y = -1;
			}
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y    ), z, d.Bottom_Block_Open);
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y + 1), z, d.Top_Block_Open);	
			
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
			if (!((b == d.Bottom_Block) || (b == d.Bottom_Block_Open)))
			{
				offset_y = -1;
			}
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y    ), z, d.Bottom_Block);
			level.UpdateBlock(Player.Console, x, (ushort)(y + offset_y + 1), z, d.Top_Block);
		}
		public void ToggleDoor(Level level, ushort x, ushort y, ushort z)
		{
			BlockID b = level.FastGetBlock((ushort)x, (ushort)(y), (ushort)z);
			DoorBlock d = GetDoorFromBlock(b);
			if ( d == null)
			{
				return;
			}
			if ( b == d.Top_Block || b == d.Bottom_Block)
			{
				OpenDoor( level, x, y, z);
			}
			else if (b == d.Top_Block_Open || b == d.Bottom_Block_Open)
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
			p.level.UpdateBlock(p, (ushort)x, (ushort)y, (ushort)z, d.Bottom_Block);
			p.level.UpdateBlock(p, (ushort)x, (ushort)(y+1), (ushort)z, d.Top_Block);
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
			if (! ( b == d.Bottom_Block || b == d.Bottom_Block_Open))
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
