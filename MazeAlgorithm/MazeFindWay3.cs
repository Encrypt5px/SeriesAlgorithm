//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MazeAlgorithm
//{
//    class MazeFindWay3
//    {
//    }
//    public class BFinding
//    {
//        protected class Point
//        {
//            public Point parent;
//            public void clear() { }

//        }

//        public BFinding()
//        {
//        }

//        protected HashSet<Point> openList = new HashSet<Point>();
//        protected HashSet<Point> leftList = new HashSet<Point>();
//        protected HashSet<Point> rightList = new HashSet<Point>();
//        protected HashSet<Point> closeList = new HashSet<Point>();

//        public synchronized ArrayList<int[]> find(Point start, Point end, bool canPenetrate)
//        {
//            if (end == null)
//            {
//                return new ArrayList<int[]>();
//            }
//            if (start == null)
//            {
//                return new ArrayList<int[]>();
//            }
//            end.clear();
//            start.clear();
//            openList.clear();
//            openList.add(start);
//            leftList.clear();
//            rightList.clear();
//            closeList.clear();

//            int count = 0;

//            while (!openList.isEmpty() || !leftList.isEmpty() || !rightList.isEmpty())
//            {
//                count++;
//                if (count > 1000)
//                    break;
//                Iterator<Point> it = openList.iterator();
//                if (it.hasNext())
//                {
//                    Point p = it.next();
//                    it.remove();
//                    if (sideNext(p, end, 0, canPenetrate)) break;
//                }
//                it = leftList.iterator();
//                if (it.hasNext())
//                {
//                    Point p = it.next();
//                    it.remove();
//                    if (sideNext(p, end, 1, canPenetrate)) break;
//                }
//                it = rightList.iterator();
//                if (it.hasNext())
//                {
//                    Point p = it.next();
//                    it.remove();
//                    if (sideNext(p, end, -1, canPenetrate)) break;
//                }
//            }
//            final ArrayList< int[] > list = new ArrayList<int[]>();
//            while (end.parent != null)
//            {
//                list.add(0, new int[] { end.x, end.y });
//                end = end.parent;
//            }
//            return list;
//        }

//        /**
//         * 
//         * @param p
//         * @param end 目标点
//         * @param side 0 direct -1 right 1 left
//         * @param canPenetrate 可否穿透
//         */
//        protected bool sideNext(Point p, Point end, int side, bool canPenetrate)
//        {
//            int dir = Point.getDirSimple(p, end);
//            Point nextp = null;

//            if (closeList.contains(p))
//            {
//                nextp = nextPassPointSide(p, end, -1, canPenetrate);
//                if (nextp != null)
//                {
//                    if (nextp == end)
//                    {
//                        nextp.parent = p;
//                        return true;
//                    }
//                    if (this.closeList.contains(nextp))
//                        //                  return sideNext(nextp, end, side, canPenetrate);
//                        return false;
//                    else if (!this.leftList.contains(nextp))
//                        addToSearch(p, nextp, this.rightList);
//                }
//                nextp = nextPassPointSide(p, end, 1, canPenetrate);
//                if (nextp != null)
//                {
//                    if (nextp == end)
//                    {
//                        nextp.parent = p;
//                        return true;
//                    }
//                    if (this.closeList.contains(nextp))
//                        //                  return sideNext(nextp, end, side, canPenetrate);
//                        return false;
//                    else if (!this.rightList.contains(nextp))
//                        addToSearch(p, nextp, this.leftList);
//                }
//                return false;
//            }
//            this.closeList.add(p);
//            if (side == 0)
//            {
//                if (p.canWalkDir(dir, canPenetrate))
//                {//下一个点可以走
//                    nextp = p.getPassPointByDir(dir);
//                    if (nextp == end)
//                    {
//                        nextp.parent = p;
//                        return true;
//                    }
//                    if (!this.closeList.contains(nextp))
//                    {
//                        addToSearch(p, nextp, this.openList);
//                    }
//                }
//                else//不可走，就分支出两个围绕探索点
//                {
//                    nextp = nextPassPointSide(p, end, -1, canPenetrate);
//                    if (nextp == end)
//                    {
//                        nextp.parent = p;
//                        return true;
//                    }
//                    if (nextp != null)
//                    {
//                        if (this.closeList.contains(nextp))
//                            return sideNext(nextp, end, side, canPenetrate);
//                        //                      return false;
//                        else if (!this.leftList.contains(nextp))
//                            addToSearch(p, nextp, this.rightList);
//                    }
//                    nextp = nextPassPointSide(p, end, 1, canPenetrate);
//                    if (nextp == end)
//                    {
//                        nextp.parent = p;
//                        return true;
//                    }
//                    if (nextp != null)
//                    {
//                        if (this.closeList.contains(nextp))
//                            return sideNext(nextp, end, side, canPenetrate);
//                        //                      return false;
//                        else if (!this.rightList.contains(nextp))
//                            addToSearch(p, nextp, this.leftList);
//                    }
//                }
//            }
//            else if (side > 0)
//            {
//                nextp = p.getPassPointByDir(dir);
//                if (nextp == end)
//                {
//                    nextp.parent = p;
//                    return true;
//                }
//                if (nextp != null && !this.closeList.contains(nextp))
//                {
//                    addToSearch(p, nextp, this.openList);
//                }
//                else
//                {
//                    nextp = nextPassPointSide(p, end, 1, canPenetrate);
//                    if (nextp == end)
//                    {
//                        nextp.parent = p;
//                        return true;
//                    }
//                    if (nextp != null && !this.closeList.contains(nextp) && !this.rightList.contains(nextp))
//                    {
//                        addToSearch(p, nextp, this.leftList);
//                    }
//                }
//            }
//            else if (side < 0)
//            {
//                nextp = p.getPassPointByDir(dir);
//                if (nextp == end)
//                {
//                    nextp.parent = p;
//                    return true;
//                }
//                if (nextp != null && !this.closeList.contains(nextp))
//                {
//                    addToSearch(p, nextp, this.openList);
//                }
//                else
//                {
//                    nextp = nextPassPointSide(p, end, -1, canPenetrate);
//                    if (nextp == end)
//                    {
//                        nextp.parent = p;
//                        return true;
//                    }
//                    if (nextp != null && !this.closeList.contains(nextp) && !this.leftList.contains(nextp))
//                    {
//                        addToSearch(p, nextp, this.rightList);
//                    }
//                }
//            }
//            return false;
//        }

//        protected void addToSearch(Point parent, Point next, HashSet<Point> list)
//        {
//            next.clear();
//            next.parent = parent;
//            list.Add(next);
//        }

//        /**
//         * 
//         * @param p
//         * @param side >0 或者 <0
//         * @param canPenetrate
//         * @return
//         */
//        protected Point nextPassPointSide(Point p, Point end, int side, bool canPenetrate)
//        {
//            int dir = Point.getDirSimple(p, end);
//            Point nextp = null;
//            if (side < 0)
//            {
//                while (side >= -7)
//                {
//                    dir = Point.rightdir(dir);
//                    if (p.canWalkDir(dir, canPenetrate))
//                    {
//                        nextp = p.getPassPointByDir(dir);
//                        if (!this.closeList.contains(nextp))
//                        {
//                            break;
//                        }
//                    }
//                    side--;
//                }
//            }
//            else
//            {
//                while (side <= 7)
//                {
//                    dir = Point.leftdir(dir);
//                    if (p.canWalkDir(dir, canPenetrate))
//                    {
//                        nextp = p.getPassPointByDir(dir);
//                        if (!this.closeList.contains(nextp))
//                        {
//                            break;
//                        }
//                    }
//                    side++;
//                }
//            }
//            return nextp;
//        }
//    }

//}
