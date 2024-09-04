import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatchService } from '../../services/match.service';
import { Match } from '../../models/match.model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.scss']
})
export class MatchListComponent implements OnInit, AfterViewInit {
  matchDays: number[] = [];
  selectedMatchDay?: number;
  matches: Match[] = [];
  filterText: string = '';
  displayedColumns: string[] = ['matchId', 'homeTeam', 'awayTeam', 'kickoffTime', 'stadium'];
  dataSource: MatTableDataSource<Match> = new MatTableDataSource<Match>();
  pageSize: number = 10;
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  @ViewChild(MatSort) sort: MatSort | null = null;
  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(private matchService: MatchService, private router: Router,private authService: AuthService) {}

  async ngOnInit() {
    try {
      this.matchDays = await this.matchService.getAvailableMatchDays();
      if (this.matchDays.length > 0) {
        this.selectedMatchDay = this.matchDays[0];
        await this.onMatchDaySelected();
      }
    } catch (error) {
      console.error('Error loading match days:', error);
    }
  }

  ngAfterViewInit() {
    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }
  }

  async onMatchDaySelected() {
    if (this.selectedMatchDay !== undefined) {
      try {
        this.matches = await this.matchService.getMatchesByMatchDay(this.selectedMatchDay);
        this.applyFilter();
      } catch (error) {
        console.error('Error fetching matches:', error);
      }
    }
  }

  applyFilter() {
    let filteredData = this.matches.filter(match =>
      match.homeTeam.teamName.toLowerCase().includes(this.filterText.toLowerCase()) ||
      match.awayTeam.teamName.toLowerCase().includes(this.filterText.toLowerCase()) ||
      match.stadium.stadiumName.toLowerCase().includes(this.filterText.toLowerCase())
    );

    if (this.sortColumn) {
      filteredData.sort((a, b) => {
        const valueA = this.getPropertyValue(a, this.sortColumn);
        const valueB = this.getPropertyValue(b, this.sortColumn);

        if (valueA < valueB) return this.sortDirection === 'asc' ? -1 : 1;
        if (valueA > valueB) return this.sortDirection === 'asc' ? 1 : -1;
        return 0;
      });
    }

    this.dataSource.data = filteredData;

    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }
  }

  changePage(event: PageEvent) {
    this.pageSize = event.pageSize;
    if (this.paginator && this.dataSource.paginator) {
      this.dataSource.paginator.pageSize = this.pageSize;
    }
  }

  handleSort(column: string) {
    if (this.sort) {
      this.sortColumn = column;
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
      this.applyFilter();
    }
  }

  getPropertyValue(obj: any, path: string): any {
    return path.split('.').reduce((o, key) => o && o[key], obj);
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}