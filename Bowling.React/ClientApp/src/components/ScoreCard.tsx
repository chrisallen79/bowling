import * as React from 'react';
import { connect } from 'react-redux';
import { Alert } from 'reactstrap';

import { ApplicationState } from '../store';
import * as GameStore from '../store/Games';

// At runtime, Redux will merge together...
type ScoreCardProps =
    GameStore.GameState // ... state we've requested from the Redux store
    & typeof GameStore.actionCreators // ... plus action creators we've requested

class ScoreCard extends React.PureComponent<ScoreCardProps> {

    constructor(props: ScoreCardProps) {
        super(props);
        const { createNewGame } = props;
        // initialize the page with a new game
        createNewGame();
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Let's Go Bowling!</h1>
                {this.renderFramesTable()}
                {this.renderFooter()}
            </React.Fragment>
        );
    }

    private isGameOver() {
        const { frames } = this.props;

        if (frames.length === 10) {
            const lastFrame = frames[frames.length - 1];
            const strike = lastFrame.roll1 === 10;
            const spare = (lastFrame.roll1 || 0) + (lastFrame.roll2 || 0) === 10;
            return lastFrame.roll3 != null || (!strike && !spare && lastFrame.roll2 != null);
        }
        return false;
    }

    private renderFramesTable() {
        const { frames } = this.props;

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Frame</th>
                        <th>Roll 1</th>
                        <th>Roll 2</th>
                        <th>Roll 3</th>
                    </tr>
                </thead>
                <tbody>
                    {frames.map((frame: GameStore.Frame) =>
                        <tr key={frame.id}>
                            <td>{frame.id}</td>
                            <td>{frame.roll1}</td>
                            <td>{frame.roll2}</td>
                            <td>{frame.roll3}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    private renderFooter() {
        const { createNewGame, gameId, isLoading, rollBall } = this.props;
        return (
            <React.Fragment>
                <p aria-live="polite">Score: <strong>{this.props.score}</strong></p>
                <Alert
                    color="primary"
                    isOpen={this.isGameOver()}>
                    Game over! Your final score was {this.props.score}!
                </Alert>
                <hr />
                <div className="panel-footer row">
                    <div className="col-6 text-left">
                        {/* disable button during loading to prevent multiple submissions */}
                        <button
                            type="button"
                            className="btn btn-primary btn-lg"
                            disabled={!(gameId > 0 && !isLoading && !this.isGameOver())}
                            onClick={() => { rollBall(gameId) }}>
                            Roll
                        </button>
                    </div>
                    <div className="col-6 text-right">
                        <button
                            type="button"
                            className="btn btn-secondary btn-lg"
                            onClick={() => { createNewGame() }}>
                            New Game
                        </button>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.games,
    GameStore.actionCreators
)(ScoreCard as any);