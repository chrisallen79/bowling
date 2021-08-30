﻿import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import ScoreCard from './components/ScoreCard';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/bowling' component={ScoreCard} />
    </Layout>
);
