namespace QUT

    module FSharpPureTicTacToeModel =
    
        // type to represent the two players: Noughts and Crosses
        type Player = Nought | Cross

        // type to represent a single move specified using (row, column) coordinates of the selected square
        type Move(_row: int, _col: int) =
            member this.row = _row
            member this.col = _col
            interface ITicTacToeMove with
                member this.Row with get() = this.row
                member this.Col with get() = this.col

        // type to represent the current state of the game, including the size of the game (NxN), who's turn it is and the pieces on the board
        type GameState(_currentPlayer: Player, _size: int, _currentRow:int, _currentCol: int) = 
            member this.currentPlayer = _currentPlayer
            member this.size = _size
            member this.currentRow = _currentRow
            member this.currentCol = _currentCol

            interface ITicTacToeGame<Player> with
                member this.Turn with get()    = this.currentPlayer
                member this.Size with get()    = this.size
                member this.getPiece(row, col) = "(" + this.currentRow.ToString() + "," + this.currentCol.ToString() + ")"


        let CreateMove row col = new Move(row, col)

        let ApplyMove (oldState:GameState) (move: Move) = 
            let nextPlayer = 
                if oldState.currentPlayer = Nought then Cross
                else Nought
            
            new GameState(nextPlayer, oldState.size, move.row, move.col)

        // Returns a sequence containing all of the lines on the board: Horizontal, Vertical and Diagonal
        // The number of lines returned should always be (size*2+2)
        // the nu                                                                                                                                                                                                                                           mber of squares in each line (represented by (row,column) coordinates) should always be equal to size
        // For example, if the input size = 2, then the output would be: 
        //     seq [seq[(0,0);(0,1)];seq[(1,0);(1,1)];seq[(0,0);(1,0)];seq[(0,1);(1,1)];seq[(0,0);(1,1)];seq[(0,1);(1,0)]]
        // The order of the lines and the order of the squares within each line does not matter
        let Lines (size:int) : seq<seq<int*int>> = 
            seq { for row in 0 .. size - 1 do
                    yield seq { for col in 0 .. size - 1 -> row, col }
                    yield seq { for col in 0 .. size - 1 -> col, row }
                    yield seq { yield row, row}
                    yield seq { yield row, size - row - 1}
                }
            

        // Checks a single line (specified as a sequence of (row,column) coordinates) to determine if one of the players
        // has won by filling all of those squares, or a Draw if the line contains at least one Nought and one Cross
        let CheckLine (game:GameState) (line:seq<int*int>) : TicTacToeOutcome<Player> = raise (System.NotImplementedException("CheckLine"))
            
        let GameOutcome game = raise (System.NotImplementedException("GameOutcome"))

        let GameStart (firstPlayer:Player) size = raise (System.NotImplementedException("GameStart"))

        let MiniMax game = raise (System.NotImplementedException("MiniMax"))

        let MiniMaxWithPruning game = raise (System.NotImplementedException("MiniMaxWithPruning"))

        // plus other helper functions ...




        [<AbstractClass>]
        type Model() =
            abstract member FindBestMove : GameState -> Move
            interface ITicTacToeModel<GameState, Move, Player> with
                member this.Cross with get()             = Cross 
                member this.Nought with get()            = Nought 
                member this.GameStart(firstPlayer, size) = GameStart firstPlayer size
                member this.CreateMove(row, col)         = CreateMove row col
                member this.GameOutcome(game)            = GameOutcome game
                member this.ApplyMove(game, move)        = ApplyMove game move 
                member this.FindBestMove(game)           = this.FindBestMove game

        type BasicMiniMax() =
            inherit Model()
            override this.ToString()         = "Pure F# with basic MiniMax";
            override this.FindBestMove(game) = raise (System.NotImplementedException("FindBestMove"))


        type WithAlphaBetaPruning() =
            inherit Model()
            override this.ToString()         = "Pure F# with Alpha Beta Pruning";
            override this.FindBestMove(game) = raise (System.NotImplementedException("FindBestMove"))