import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Match } from '../models/match.model';
import { Team } from '../models/team.model';
import { Stadium } from '../models/stadium.model';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  //private apiUrl = 'https://localhost:5001/api/Matches'; 
  private apiUrl =  'http://localhost:8080/api/Matches'
  
  constructor(private http: HttpClient) {}
 
  async getAvailableMatchDays(): Promise<number[]> {
    return (await firstValueFrom(this.http.get<number[]>(`${this.apiUrl}/matchdays`))) || [];
  }

 async getMatchesByMatchDay(matchDay: number): Promise<Match[]> {
    return (await firstValueFrom(
      this.http.get<Match[]>(`${this.apiUrl}/matchday/${matchDay}`).pipe(
        map(matches => {
          if (!matches) {
            return [];
          }
          return matches.map(match => this.mapMatchDto(match));
        })
      )
    ));
  }
  private mapMatchDto(match: any): Match {
    const defaultTeam: Team = { teamId: '', teamName: 'Unknown' };
    const defaultStadium: Stadium = { stadiumId: '', stadiumName: 'Unknown' };
    return {
      matchId: match.matchId,
      dlProviderId: match.dlProviderId,
      matchDay: match.matchDay,
      competitionId: match.competitionId,
      kickoffTime: new Date(match.kickoffTime),
      homeTeam: match.homeTeamDto ? { teamId: match.homeTeamDto.teamId, teamName: match.homeTeamDto.teamName } : defaultTeam,
      awayTeam: match.awayTeamDto ? { teamId: match.awayTeamDto.teamId, teamName: match.awayTeamDto.teamName } : defaultTeam,
      stadium: match.stadiumDto ? { stadiumId: match.stadiumDto.stadiumId, stadiumName: match.stadiumDto.stadiumName } : defaultStadium
    };
  }
}