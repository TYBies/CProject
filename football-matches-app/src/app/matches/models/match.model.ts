import { Stadium } from "./stadium.model";
import { Team } from "./team.model";

// matches/models/match.model.ts
export interface Match {
    matchId: string;
    matchDay: number;
    competitionId: number;
    dlProviderId: number;
    kickoffTime: Date;
    homeTeam: Team;
    awayTeam: Team;
    stadium: Stadium;
  }