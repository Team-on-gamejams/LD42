using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ld42 {
	class Snake {
		Direction newDir;
		List<Hero> body;

		public Snake() {
			body = new List<Hero>(4);
			newDir = Settings.snakeStartDirection;
		}

		public void ProcessShake() {
			for (int i = 0; i < body.Count; ++i)
				++body[i].tickOnCell;

			if (Settings.tick % Settings.ticksForMove == 0) {
				for (byte i = (byte)(body.Count - 1); i > 0; --i) {
					body[i].pos.X = body[i - 1].pos.X;
					body[i].pos.Y = body[i - 1].pos.Y;

					body[i].dir = body[i - 1].dir;
				}

				body[0].dir = newDir;
				if (body[0].dir == Direction.Left) {
					--body[0].pos.X;
					Settings.game.gameMap.MoveCamLeft();
				}
				else if (body[0].dir == Direction.Right) {
					++body[0].pos.X;
					Settings.game.gameMap.MoveCamRight();
				}
				else if (body[0].dir == Direction.Up) {
					--body[0].pos.Y;
					Settings.game.gameMap.MoveCamUp();
				}
				else if (body[0].dir == Direction.Down) {
					++body[0].pos.Y;
					Settings.game.gameMap.MoveCamDown();
				}

				Canvas.SetLeft(body[0].image, Settings.cellSize.X * Settings.snakeStartPos.X);
				Canvas.SetTop(body[0].image,  Settings.cellSize.Y * Settings.snakeStartPos.Y);
				for (int i = 1; i < body.Count; ++i) {
					Canvas.SetLeft(body[i].image, Settings.cellSize.X * (body[i].pos.X - body[0].pos.X + Settings.snakeStartPos.X));
					Canvas.SetTop(body[i].image,  Settings.cellSize.Y * (body[i].pos.Y - body[0].pos.Y + Settings.snakeStartPos.Y));
				}
			}
		}

		public void FireAt(double x, double y) {
			foreach (var hero in body) {
				CoordReal heroPos = new CoordReal {
					X = Canvas.GetLeft(hero.image) + hero.image.DesiredSize.Width / 2,
					Y = Canvas.GetTop(hero.image) + hero.image.DesiredSize.Height  / 2,
				};
				double angle = Math.Atan2(y - heroPos.Y, x - heroPos.X) - Math.Atan2(0, 1);
				angle = (angle * 180.0 / 3.14159);
				hero.Shoot(angle, heroPos);
			}
		}

		public bool MoveLeft() {
			if (body[0].dir == Direction.Right)
				return false;
			newDir = Direction.Left;
			return true;
		}

		public bool MoveUp() {
			if (body[0].dir == Direction.Down)
				return false;
			newDir = Direction.Up;
			return true;
		}

		public bool MoveRight() {
			if (body[0].dir == Direction.Left)
				return false;
			newDir = Direction.Right;
			return true;
		}

		public bool MoveDown() {
			if (body[0].dir == Direction.Up)
				return false;
			newDir = Direction.Down;
			return true;
		}

		public void AddHero(Hero hero) {
			body.Add(hero);
			if(body.Count == 1) {
				hero.pos.X = Settings.snakeStartPos.X;
				hero.pos.Y = Settings.snakeStartPos.Y;
				hero.dir = Settings.snakeStartDirection;
			}
			else {
				hero.dir = body[body.Count - 2].dir;
				hero.pos.X = body[body.Count - 2].pos.X;
				hero.pos.Y = body[body.Count - 2].pos.Y;
				if (hero.dir == Direction.Left)
					++hero.pos.X;
				else if (hero.dir == Direction.Right)
					--hero.pos.X;
				else if (hero.dir == Direction.Up)
					++hero.pos.Y;
				else if (hero.dir == Direction.Down)
					--hero.pos.Y;
			}

			Settings.gameWindow.GameCanvas.Children.Add(hero.image);
			Canvas.SetZIndex(hero.image, 5);
			Canvas.SetLeft(hero.image, Settings.cellSize.X * (hero.pos.X - Settings.game.gameMap.CamPos.X));
			Canvas.SetTop(hero.image, Settings.cellSize.Y * (hero.pos.Y - Settings.game.gameMap.CamPos.Y));

			/*
			hero.pos.Changed += (xChange, yChange) => {
				if(xChange != 0)
					Canvas.SetLeft(hero.image, Settings.cellSize.X * (hero.pos.X + xChange - Settings.game.gameMap.CamPos.X));
				if(yChange != 0)
					Canvas.SetTop(hero.image,  Settings.cellSize.Y * (hero.pos.Y + yChange - Settings.game.gameMap.CamPos.Y));
			};
			
			Settings.game.gameMap.CamPos.SizeChanged += (xChange, yChange) => {
				if (xChange != 0)
					Canvas.SetLeft(hero.image, Settings.cellSize.X * (hero.pos.X - Settings.game.gameMap.CamPos.X));
				if (yChange != 0)
					Canvas.SetTop(hero.image, Settings.cellSize.Y * (hero.pos.Y  - Settings.game.gameMap.CamPos.Y));
			};
			*/
		}
	}
}
