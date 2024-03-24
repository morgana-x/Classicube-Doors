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
				Item_Block = 66, // t id 182
				Top_Block = 69,
				Bottom_Block = 67,
				Top_Block_Open = 70,
				Bottom_Block_Open = 68
				
			},
			new DoorBlock() // test
			{
				Item_Block = 6, // t id 182
				Top_Block = 22,
				Bottom_Block = 24,
				Top_Block_Open = 23,
				Bottom_Block_Open = 25
				
			}
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
			if (!placing)
			{
				return;
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
				if (!IsDoor(p.level,nx,ny,nz))
				{
					if (i == 4)
					{
						if (IsDoorItem(block))
						{
							PlaceDoor(p, x, y, z, block);
							cancel = true;
							p.RevertBlock(x, y, z);
						}
						return;
					}
				}
				else
				{
					break;
				}
			}
			cancel = true;
			p.RevertBlock(x, y, z);
			ToggleDoor(p.level, nx, ny, nz);
			/*
			p.level.UpdateBlock(p, (ushort)nx, (ushort)ny, (ushort)nz, 0);
			Server.MainScheduler.QueueOnce( (SchedulerTask task2) => {
				p.level.UpdateBlock(p, (ushort)nx,   (ushort)ny, (ushort)nz, 64);
			}, null, TimeSpan.FromSeconds(2));*/
        }
	}
}