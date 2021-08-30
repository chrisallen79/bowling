import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface GameState {
    isLoading: boolean;
    gameId: number;
    frames: Frame[];
    score: number;
}

export interface Frame {
    id: number;
    roll1: number | null;
    roll2: number | null;
    roll3: number | null;
}

export interface ApiFrame {
    id: number;
    frameSequence: number,
    roll1: number | null;
    roll2: number | null;
    roll3: number | null;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestNewGameAction {
    type: 'REQUEST_NEW_GAME';
}

interface ReceiveNewGameAction {
    type: 'RECEIVE_NEW_GAME';
    gameId: number;
}

interface RequestGameAction {
    type: 'REQUEST_GAME';
}

interface ReceiveGameAction {
    type: 'RECEIVE_GAME';
    frames: Frame[];
}

interface RequestRollAction {
    type: 'REQUEST_ROLL';
}

interface ReceiveRollAction {
    type: 'RECEIVE_ROLL';
    frame: Frame;
}

interface RequestScoreAction {
    type: 'REQUEST_SCORE';
}

interface ReceiveScoreAction {
    type: 'RECEIVE_SCORE';
    score: number;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction =
    RequestNewGameAction |
    ReceiveNewGameAction |
    RequestGameAction |
    ReceiveGameAction |
    RequestRollAction |
    ReceiveRollAction |
    RequestScoreAction |
    ReceiveScoreAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    createNewGame: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState) {
            fetch(`bowling/games/new`, { method: 'POST' })
                .then(response => response.json() as Promise<GameState>)
                .then(data => {
                    dispatch(
                        {
                            type: 'RECEIVE_NEW_GAME',
                            gameId: data.gameId
                        });
                });

            dispatch({ type: 'REQUEST_NEW_GAME' });
        }
    },
    rollBall: (gameId: number): AppThunkAction<KnownAction> => async (dispatch, getState) => {
        const appState = getState();
        if (appState && gameId && gameId > 0) {
            fetch(`bowling/games/${gameId}/roll`)
                .then(response => response.json() as Promise<ApiFrame>)
                .then(data => {
                    return dispatch(
                        {
                            type: 'RECEIVE_ROLL',
                            frame: {
                                id: data.frameSequence,
                                roll1: data.roll1,
                                roll2: data.roll2,
                                roll3: data.roll3
                            }
                        });
                })
                .then(() => {
                    fetch(`bowling/games/${gameId}/score`)
                        .then(response => response.json() as Promise<number>)
                        .then(data => {
                            dispatch({ type: 'RECEIVE_SCORE', score: data });
                        });

                    dispatch({ type: 'REQUEST_SCORE' });
                });

            dispatch({ type: 'REQUEST_ROLL' });
        }
    }
};

const unloadedState: GameState =
{
    gameId: -1,
    frames: [
        { id: 1, roll1: null, roll2: null, roll3: null },
        { id: 2, roll1: null, roll2: null, roll3: null },
        { id: 3, roll1: null, roll2: null, roll3: null },
        { id: 4, roll1: null, roll2: null, roll3: null },
        { id: 5, roll1: null, roll2: null, roll3: null },
        { id: 6, roll1: null, roll2: null, roll3: null },
        { id: 7, roll1: null, roll2: null, roll3: null },
        { id: 8, roll1: null, roll2: null, roll3: null },
        { id: 9, roll1: null, roll2: null, roll3: null },
        { id: 10, roll1: null, roll2: null, roll3: null }
    ],
    isLoading: false,
    score: 0
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<GameState> = (state: GameState | undefined, incomingAction: Action): GameState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_NEW_GAME':
            return {
                ...state,
                isLoading: true
            };
        case 'RECEIVE_NEW_GAME':
            return {
                ...unloadedState,
                gameId: action.gameId,
                isLoading: false
            };
        case 'REQUEST_GAME':
            return {
                ...state,
                frames: state.frames,
                isLoading: true
            };
        case 'RECEIVE_GAME':
            return {
                ...state,
                frames: action.frames,
                isLoading: false
            };
        case 'REQUEST_ROLL':
            return {
                ...state,
                isLoading: true
            };
        case 'RECEIVE_ROLL':
            return {
                ...state,
                frames: [
                    ...state.frames.filter(f => f.id < action.frame.id),
                    action.frame,
                    ...state.frames.filter(f => f.id > action.frame.id)
                ],
                isLoading: false
            };
        case 'REQUEST_SCORE':
            return {
                ...state,
                score: state.score,
                isLoading: true
            };
        case 'RECEIVE_SCORE':
            return {
                ...state,
                score: action.score,
                isLoading: false
            };
    }

    return state;
};
